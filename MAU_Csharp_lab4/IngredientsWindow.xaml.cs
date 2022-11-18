using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MAU_Csharp_lab4
{
    /// <summary>
    /// Interaction logic for IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        // Store the added ingredients in a local array
        private string[] tempIngredients;

        // Import existing recipe to import it's existing ingredients, in case of editing existing recipe
        private Recipe currentRecipe;

        // Bool to control what happens when user clicks add / change -button
        private bool isEditing = false;

        // Keep track of what element in the recipe array that is being edited
        private int editingIndex;


        // Pass in the current recipe in creation from main window
        public IngredientsWindow(Recipe recipe)
        {            
            InitializeComponent();
            this.currentRecipe = recipe;

            // Set the temporary array to correct size defined in main window
            tempIngredients = new string[recipe.MaxNumberOfIngredients];

            // Import existing ingredients from recipe object to tempIngredients and listbox
            ImportExistingIngredientsFromRecipe(recipe);

            // Disable buttons as default
            DisableButtons();                        
            UpdateListbox();
        }


        // Disable buttons add, edit, delete, ok
        private void DisableButtons()
        {
            btn_add.IsEnabled = false;
            btn_edit.IsEnabled = false;
            btn_delete.IsEnabled = false;
            btn_ok.IsEnabled = false;
        }


        // Import the existing recipe ingredients to the local array
        private void ImportExistingIngredientsFromRecipe(Recipe recipe)
        {
            for (int i = 0; i < tempIngredients.Length; i++)
            {
                tempIngredients[i] = recipe.Ingredients[i];
            }
        }


        // Add ingredient - button click
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                tempIngredients[editingIndex] = tbx_ingredient.Text;
                isEditing = false;

                // Change button back to "add"
                btn_add.Content = "Add";
                tbx_ingredient.Clear();
            }            
            else if (AddIngredientToArray())
            {
                tbx_ingredient.Clear();
            }
            else MessageBox.Show("Cannot add more ingredients");
            UpdateListbox();
            SetOkButton();
        }


        // Disable Ok-button if ingredient list is empty
        private void SetOkButton()
        {
            if (tempIngredients.Length == 0)
                btn_ok.IsEnabled = false;
            else btn_ok.IsEnabled = true;
        }


        // Edit ingredient - button click
        private void Edit_Click(object sender, RoutedEventArgs e)
        {   
            isEditing = true;
            editingIndex = lbx_ingredientList.SelectedIndex;
            btn_add.Content = "Change";
            tbx_ingredient.Text = lbx_ingredientList.SelectedItem.ToString();
            lbx_ingredientList.SelectedItem = null;
        }

        // Delete ingredient - button click
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            tempIngredients[lbx_ingredientList.SelectedIndex] = "";
            //FixEmptyElementsInIngredientsArray();
            UpdateListbox();
            SetOkButton();
        }


        // Updates the listbox of ingredients to show changes/existing ingredients
        private void UpdateListbox()
        {
            // Remove empty elements
            FixEmptyElementsInIngredientsArray();

            lbx_ingredientList.ItemsSource = null;
            lbx_ingredientList.ItemsSource = tempIngredients;
        }
        
        
        // Ingredient textbox change
        private void IngredientTextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_ingredient.Text == "" || tbx_ingredient.Text == null)
                btn_add.IsEnabled = false;
            else btn_add.IsEnabled = true;
        }


        // Listbox selection changed
        private void ListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable / disable buttons
            if (lbx_ingredientList.SelectedItem == null)
            {
                btn_edit.IsEnabled = false;
                btn_delete.IsEnabled = false;
            }
            else
            {
                btn_edit.IsEnabled = true;
                btn_delete.IsEnabled = true;
            }
        }


        // Add ingredient element to the array
        private bool AddIngredientToArray()
        {
            for (int i = 0; i < tempIngredients.Length; i++)
            {
                if (tempIngredients[i] == "" || tempIngredients[i] == null)
                {
                    tempIngredients[i] = tbx_ingredient.Text;
                    return true;
                }                
            }
            return false;
        }


        // OK - button click (add ingredients to recipe and close window)
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe.AddIngredients(tempIngredients);
            this.Close();
        }


        // Cancel -button click
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // Remove empty elements in the array in case of deletion
        private void FixEmptyElementsInIngredientsArray()
        {
            // Look for empty element
            for (int i = 0; i < tempIngredients.Length; i++)
            {
                if (tempIngredients[i] == null || tempIngredients[i] == "")
                {
                    for (int j = i; j < tempIngredients.Length - 1; j++)
                    {
                        tempIngredients[j] = tempIngredients[j + 1];
                    }
                }
            }
        }
    }
}
