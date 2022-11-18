using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MAU_Csharp_lab4
{
    public class Recipe
    {
        //public int NumberOfIngredients = 0;
        private int maxNumberOfIngredients;
        public int MaxNumberOfIngredients 
        {
            get { return maxNumberOfIngredients; }
        }

        private string[] ingredients;       
        private string description;
        public string Description
        {
            get { return description; }
        }
        private string category;
        public string Category { get { return category; } }
        private int categoryIndex;
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string GetIngredient(int i)
        {
            return ingredients[i];
        }

        

        // Constructor
        public Recipe(int maxNumberOfIngredients)
        {
            this.maxNumberOfIngredients = maxNumberOfIngredients;
            ingredients = new string[maxNumberOfIngredients];
        }


        // Adds the ingredients array to the object
        public void AddIngredients(string[] ingredients)
        {
            this.ingredients = ingredients;
        }

        public void AddDescription(string description)
        {
            this.description = description;
        }

        public void AddCategory(int categoryIndex)
        {
            // Pass in the selected combobox index and set the recipe category string
            this.category = ((FoodCategory)categoryIndex).ToString();
            this.categoryIndex = categoryIndex;
        }

        public int GetCategoryIndex()
        {
            return this.categoryIndex;
        }

        //public void AddCategory(string category)
        //{
        //    this.Category = category;
        //}

        public int GetNumberOfIngredients()
        {
            int numberOfIngredients = 0;
            foreach (var str in ingredients)
            {
                if (str != null && str.Length > 0)
                    numberOfIngredients++;
            }
            return numberOfIngredients;
        }

        public void ShowRecipeInfo()
        {
            Window recipeInfo = new RecipeInfoWindow(GetRecipeInfo());
            recipeInfo.Title = Name;
            recipeInfo.Show();
        }


        private string GetRecipeInfo()
        {
            string recipeInfo = "INGREDIENTS:\n";
            foreach (var str in ingredients)
            {
                if (str == null || str == "")
                    continue;
                recipeInfo += $"{str}\n";
            }
            recipeInfo += $"\nDESCRIPTION:\n{description}\n";         
            return recipeInfo;
        }
    }
}
