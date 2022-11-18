using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;

namespace MAU_Csharp_lab4
{
    public class RecipeManager
    {
        private Recipe[] allRecipes;
        private int maxNumberOfRecipes;
        private int numberOfRecipes = 0;
        public RecipeManager(int maxNumberOfRecipes)
        {
            this.maxNumberOfRecipes = maxNumberOfRecipes;
            allRecipes = new Recipe[maxNumberOfRecipes];
        }


        public bool AddRecipe(Recipe recipe)
        {          
            if (recipe == null)
                return false;
            allRecipes[numberOfRecipes] = recipe;
            numberOfRecipes++;
            return true;    
        }


        public void RemoveRecipe(int index)
        {
            allRecipes[index] = null;
            RemoveSpaces();
            numberOfRecipes--;
        }


        public void EditRecipe(Recipe recipe, int index)
        {
            
            allRecipes[index] = recipe;
        }       


        public string[] GetRecipesNames()
        {
            return allRecipes.Where(recipe => recipe != null).Select(recipe => recipe.Name).ToArray();            
        }


        public string[] GetRecipesCategory()
        {
            return allRecipes.Where(recipe => recipe != null).Select(recipe => recipe.Category).ToArray();
        }


        public int[] GetRecipesNumberOfIngredients()
        {
            return allRecipes.Where(recipe => recipe != null).Select(recipe => recipe.GetNumberOfIngredients()).ToArray();
        }

        public Recipe GetRecipeAt(int index)
        {
            return allRecipes[index];
        }


        // Remove spaces in allRecipes in order to add next recipe at the end 
        private void RemoveSpaces()
        {
            for (int i = 0; i < allRecipes.Length; i++)
            {
                if (allRecipes[i] == null)
                {
                    for (int j = i; j < allRecipes.Length - 1; j++)
                    {
                        allRecipes[j] = allRecipes[j + 1];
                    }
                }
            }
        }
    }  
}
