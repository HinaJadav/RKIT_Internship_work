using EmployeeManagementView.Views;

class Program
{
    /// <summary>
    /// The entry point of the Employee Management System console application.
    /// Initializes the Employee Management View and runs the application.
    /// </summary>
    static void Main(string[] args)
    {
        var view = new EmployeeView();
        view.Run();
    }
}
