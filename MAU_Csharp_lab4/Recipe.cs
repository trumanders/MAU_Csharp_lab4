using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MAU_Csharp_lab4
{
    public class Recipe
    {
        public int NumberOfIngredients = 0;
        public int MaxNumberOfIngredients { get; }
        public string[] Ingredients { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public int CategoryIndex { get; private set; }
        public string Name { get; set; }

        public Recipe(int maxNumberOfIngredients)
        {
            this.MaxNumberOfIngredients = maxNumberOfIngredients;
            Ingredients = new string[maxNumberOfIngredients];
        }


        // Adds the ingredients array to the object
        public void AddIngredients(string[] ingredients)
        {
            this.Ingredients = ingredients;
        }

        public void AddDescription(string description)
        {
            this.Description = description;
        }

        public void AddCategory(int categoryIndex)
        {
            // Pass in the selected combobox index and set the recipe category string
            this.Category = ((FoodCategory)categoryIndex).ToString();
            this.CategoryIndex = categoryIndex;
        }

        public int GetCategoryIndex()
        {
            return this.CategoryIndex;
        }

        //public void AddCategory(string category)
        //{
        //    this.Category = category;
        //}

        public int GetNumberOfIngredients()
        {
            int numberOfIngredients = 0;
            foreach (var str in Ingredients)
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
            foreach (var str in Ingredients)
            {
                if (str == null || str == "")
                    continue;
                recipeInfo += $"{str}\n";
            }
            recipeInfo += $"\nDESCRIPTION:\n{Description}\n";         
            return recipeInfo;
        }
    }
}
