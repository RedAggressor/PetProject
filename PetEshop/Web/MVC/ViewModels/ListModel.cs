namespace MVC.ViewModels
{
    public class ListModel<T> : ErrorViewModel
    {
        public IEnumerable<T>? List { get; set; }
    }
}
