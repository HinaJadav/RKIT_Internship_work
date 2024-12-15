using WebAPIProject.Views;

class Program
{
    /// <summary>
    /// The entry point of the Employee Management System console application.
    /// Initializes the EmployeeView and runs the application.
    /// </summary>
    static void Main(string[] args)
    {
        var view = new EmployeeView();
        view.Run();
    }
}
