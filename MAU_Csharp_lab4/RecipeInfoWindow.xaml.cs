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
    /// Interaction logic for RecipeInfoWindow.xaml
    /// </summary>
    public partial class RecipeInfoWindow : Window
    {        
        public RecipeInfoWindow(string recipeInfo)
        {
            InitializeComponent();
            tbl_recipeInfo.Text = recipeInfo;                     
        }
    }
}