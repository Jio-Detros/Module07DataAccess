using Module07DataAccess.Services;
using Module07DataAccess.ViewModel;

namespace Module07DataAccess.View
{
    public partial class ViewEmployee : ContentPage // Updated Class Name
    {
        public ViewEmployee()
        {
            InitializeComponent();

            var employeeViewModel = new EmployeeViewModel(); // Updated ViewModel
            BindingContext = employeeViewModel;
        }
    }
}
