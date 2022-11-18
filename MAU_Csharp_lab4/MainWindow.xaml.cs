using System.Windows;
using System.Windows.Controls;

namespace MAU_Csharp_lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int maxNumberOfRecipes = 50;
        private const int MAX_NUMBER_OF_INGREDIENTS = 30;
        private Recipe currentRecipe;
        private RecipeManager recipeManager;
        private bool isEditing = false;
        private int editingIndex = -1;


        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager(maxNumberOfRecipes);
            currentRecipe = new Recipe(MAX_NUMBER_OF_INGREDIENTS);

            // Disable buttons as default
            DisableButtonsDefault();
        }


        // ADD INGREDIENTS BUTTON CLICK
        private void btn_AddIngredients_Click(object sender, RoutedEventArgs e)
        {                    
            IngredientsWindow ingredientsWindow = new IngredientsWindow(currentRecipe);
            ingredientsWindow.Show();
        }


        // Recipe name-textbox content changed
        private void RecipeNameChange(object sender, TextChangedEventArgs e)
        {
            ToggleAddButtons();
        }


        // Category selection changed
        private void CategoryChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleAddButtons();
        }


        private void DescriptionChanged(object sender, TextChangedEventArgs e)
        {
            ToggleAddButtons();
        }


        // Enable Add recipe -button if name, category and ingredients are entered
        private void ToggleAddButtons()
        {
            if ((tbx_inputRecipeName.Text != null && tbx_inputRecipeName.Text != "") &&
                (cbx_category.SelectedItem != null && cbx_category.SelectedItem.ToString() != ""))
            {
                btn_addIngredients.IsEnabled = true;
                if (tbx_inputRecipeDescription.Text != null && tbx_inputRecipeDescription.Text != "")
                    btn_addRecipe.IsEnabled = true;
                else btn_addRecipe.IsEnabled = false;
            }
            else btn_addIngredients.IsEnabled = false;
        }


        // Add recipe!
        private void btn_AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe.Name = tbx_inputRecipeName.Text;
            currentRecipe.AddDescription(tbx_inputRecipeDescription.Text);
            currentRecipe.AddCategory(cbx_category.SelectedIndex);

            if (isEditing)
            {
                btn_addRecipe.Content = "Add recipe";
                recipeManager.EditRecipe(currentRecipe, editingIndex);
                isEditing = false;                
            }
            else
            {
                if (!recipeManager.AddRecipe(currentRecipe))
                    MessageBox.Show("Could not add recipe");
            }        

            // Set Recipe Listbox
            UpdateRecipesList();
            ClearTextboxes();
        }


        private void btn_EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            editingIndex = lbx_recipesName.SelectedIndex;
            isEditing = true;

            // Disable buttons while editing
            btn_edit.IsEnabled = false;
            btn_delete.IsEnabled = false;   

            // Set textboxes and combobox to the selected recipe
            currentRecipe = recipeManager.GetRecipeAt(editingIndex);
            tbx_inputRecipeName.Text = currentRecipe.Name;
            cbx_category.SelectedIndex = currentRecipe.GetCategoryIndex();
            tbx_inputRecipeDescription.Text = currentRecipe.Description;
            btn_addRecipe.Content = "Done editing";
        }


        private void btn_DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            recipeManager.RemoveRecipe(lbx_recipesName.SelectedIndex);
            UpdateRecipesList();
            lbx_recipesName.SelectedItem = null;
        }


        private void ClearTextboxes()
        {
            tbx_inputRecipeDescription.Text = null;
            tbx_inputRecipeName.Text = null;
            cbx_category.SelectedItem = null;
            currentRecipe = new Recipe(MAX_NUMBER_OF_INGREDIENTS);
        }


        private void btn_ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            lbx_recipesName.SelectedItem = null;
        }


        // Added recipes selection changed
        private void RecipeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            if (lbx_recipesName.SelectedItem == null)
            {
                btn_delete.IsEnabled = false;
                btn_edit.IsEnabled = false;
            }
            else if (!isEditing) 
            {
                btn_delete.IsEnabled = true;
                btn_edit.IsEnabled = true;                
            }
        }

        
        private void RecipeDoubleClick(object sender, RoutedEventArgs e)
        {            
            if (lbx_recipesName.SelectedItem == null)
                return;

            // Call method to show recipe info by passing the selected recipe
            recipeManager.GetRecipeAt(lbx_recipesName.SelectedIndex).ShowRecipeInfo();
        }
        

        private void DisableButtonsDefault()
        {
            btn_addIngredients.IsEnabled = false;
            btn_addRecipe.IsEnabled = false;
            btn_delete.IsEnabled = false;
            btn_edit.IsEnabled = false;            
        }


        private void UpdateRecipesList()
        {
            lbx_recipesName.ItemsSource = null;
            lbx_recipesName.ItemsSource = recipeManager.GetRecipesNames();
            lbx_recipesCategory.ItemsSource = recipeManager.GetRecipesCategory();
            lbx_recipeNumOfIngredients.ItemsSource = recipeManager.GetRecipesNumberOfIngredients();
        }
    }
}
