using DCS.CoreLib.View;
using DCS.User.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DCS.User.Views
{
    /// <summary>
    /// Interaction logic for ChangeUserPassword.xaml
    /// </summary>
    public partial class ChangeUserPassword : DefaultEditorWindow
    {
        private readonly IUserService userService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUserService>();
        private UserViewModel viewModel;

        /// <summary>
        /// Default constructor initialize a new <see cref="ChangeUserPassword"/> window.
        /// </summary>
        /// <param name="user">Selected user.</param>
        public ChangeUserPassword(User user) : base(user)
        {
            InitializeComponent();

            viewModel = new UserViewModel(user);
            this.DataContext = viewModel;
        }


    }
}
