using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agoratopia.Database;
using SQLite;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TierSetting : ContentPage
    {
        string[] questions = { "Welcome to Agoratopia! Since this is your first time on the app, we're going to ask a few questions to customize the app to your experience.",
                               "What is your name?",
                               "How long have you had agoraphobia?",
                               "How many \"tiers\" of exposure therapy are you going through?" ,
                               "Can you label these tiers? (e.g. Tier 1 = outside the house, \nTier 2 = Grocery store, Tier 3 = 10 miles from home, etc.)" };

        int questionCount = 0;

        public TierSetting()
        {
            InitializeComponent();

            MainText.Text = questions[0];
        }

        async public void Transition(object sender, System.EventArgs e)
        {

            // Fade out transition
            await QFrame.FadeTo((double)0);

            switch ((string)((Button)sender).BindingContext)
            {
                case "Back": // Transition backwards

                    if (questionCount > 0)
                    {
                        questionCount--;

                        MainText.Text = questions[questionCount];
                    }

                    // Disable back button when there's nowhere left to go
                    if (questionCount == 0)
                        Disable(BackButton);

                    if (SubmitButton.IsEnabled)
                        Disable(SubmitButton);

                    break;
                case "Forward": // Transition forward
                    // Only advance if questionCount is less than the number of questions
                    if (questionCount < questions.Length - 1)
                    {
                        questionCount++;

                        MainText.Text = questions[questionCount];
                    }
                    else if (questionCount == questions.Length - 1)
                    {
                        questionCount++;
                        Switch(SubmitButton);
                        Disable(Q4flex);
                        Confirm();
                    }

                    // Enable back button if it's disabled
                    if (questionCount > 0)
                    {
                        Switch(BackButton);
                        BackButton.BackgroundColor = Color.Red;
                    }

                    break;
                default:
                    break;
            }

            // Decide what input fields are visible
            switch (questionCount)
            {
                case 0:
                    Disable(Q1field);

                    break;
                case 1:
                    Switch(Q1field);

                    Disable(Q2field);

                    break;
                case 2:
                    // Disable NameEntry, enable NumTiers
                    Switch(Q2field);

                    // Make sure the fields in front AND behind are inactive
                    Disable(Q1field);
                    Disable(Q3field);

                    break;
                case 3:
                    // Disable NumTiers, enable the next
                    Disable(Q2field);
                    Disable(Q4flex);

                    Switch(Q3field);
                    
                    break;
                case 4:
                    Disable(Q3field);

                    Switch(Q4flex);

                    // Kill all the children each time. It's the only way.
                    Q4flex.Children.Clear();
                    System.Console.WriteLine("WARNING: CHILDREN MURDERED");

                    // Add the same number of entries as indicated in Q3
                    for (int i = 0; i < int.Parse((string)Q3field.SelectedItem); i++)
                    {
                        Grid innerGrid = new Grid
                        {
                            ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = new GridLength(0.30, GridUnitType.Star) },
                                new ColumnDefinition { Width = new GridLength(0.70, GridUnitType.Star) }
                            },
                            ColumnSpacing = 2.0
                        };

                        Label label = new Label
                        {
                            Text = $"Tier {i + 1}:",
                            TextColor = Color.White,
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                        };

                        Entry entry = new Entry
                        {
                            Placeholder = "Tier here",
                            BackgroundColor = Color.White,
                            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry))
                        };

                        Grid.SetColumn(label, 0);
                        Grid.SetColumn(entry, 1);

                        innerGrid.Children.Add(label);
                        innerGrid.Children.Add(entry);

                        Q4flex.Children.Add(innerGrid);
                                                
                    }

                    break;
                default:
                    System.Console.WriteLine("DISABLED");
                    break;
            }

            // Fade in transition
            await QFrame.FadeTo((double)1);

        }


        // Method to enable/disable objects
        private void Switch(View entryField)
        {
            if (entryField != null)
            {
                entryField.IsVisible = true;
                entryField.IsEnabled = true;
            }

        }

        private void Switch(View entryField1, View entryField2)
        {
            if (entryField1 != null && entryField2 != null)
            {
                entryField1.IsVisible = !entryField1.IsVisible;
                entryField1.IsEnabled = !entryField1.IsEnabled;

                entryField2.IsVisible = !entryField2.IsVisible;
                entryField2.IsEnabled = !entryField2.IsEnabled;
            }
        }

        private void Disable(View entryField)
        {
            if (entryField != null)
            {
                entryField.IsVisible = false;
                entryField.IsEnabled = false;
            }
        }

        async private void Submit(object sender, System.EventArgs e)
        {
            Settings settings = new Settings
            {
                Name = Q1field.Text,
                AgoraLength = $"{AgoraNum.SelectedItem} {MonthOrYear.SelectedItem}",
                NumOfTiers = int.Parse(Q3field.SelectedItem.ToString())
            };

            TierLabels[] tierLabels = new TierLabels[settings.NumOfTiers];

            System.Console.WriteLine("HUIODASFFPOIJASCIPOFUJPOIWSADJIOFPEDSJA");

            for (int i = 0; i < settings.NumOfTiers; i++)
            {
                // WHY WASN'T I INITIALIZING THIS BEFORE I'M STUPID
                tierLabels[i] = new TierLabels();

                tierLabels[i].TierLabel = ((Entry)((Grid)Q4flex.Children[i]).Children[1]).Text;
            }

            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                // Make sure a table actually exists
                conn.CreateTable<Settings>();
                conn.CreateTable<TierLabels>();

                conn.Insert(settings);


                foreach(TierLabels label in tierLabels)
                    conn.Insert(label);

                conn.Close();
            }

            await Navigation.PushAsync(new TestPage());
        }

        private void Confirm()
        {
            MainText.Text = $"Hello {Q1field.Text}! I see that you've had agoraphobia for {AgoraNum.SelectedItem} {MonthOrYear.SelectedItem}, with {Q3field.SelectedItem} tiers of therapy to go through. Is this correct?\n";
            //MainText.Text = ((FlexLayout)Q4flex.Children[0]).Children[1].ToString();

            for (int i = 0; i < int.Parse(Q3field.SelectedItem.ToString()); i++)
            {
                MainText.Text += $"\nTier {i+1}: {((Entry)((Grid)Q4flex.Children[i]).Children[1]).Text}";
            }
        }
    }
}