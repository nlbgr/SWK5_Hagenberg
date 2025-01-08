using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_1
{
    public class ExamResult (string name, int grade)
    {
        public string Name { get; set; } = name;
        public int Grade { get; set; } = grade;
    }
    public delegate void ResultAddedHandler(ExamResult result);
    public interface IExamLogic
    {
        event ResultAddedHandler ResultAdded; // notifies subscribers about new ExamResults
    }

    public class ExamLogic : IExamLogic
    {
        public event ResultAddedHandler? ResultAdded;
    }

    public class ExamMonitorVM
    {
        public ObservableCollection<ExamResult> Results { get; set; } = [
            new (name: "a", grade: 1),
            new (name: "b", grade: 2),
            new (name: "c", grade: 3),
            new (name: "d", grade: 4),
        ];
        private readonly IExamLogic logic;

        public ExamMonitorVM(IExamLogic logic)
        {
            this.logic = logic;
            this.logic.ResultAdded += (r) => Results.Add(r);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (o, r) => this.DataContext = new ExamMonitorVM(new ExamLogic());
        }
    }
}