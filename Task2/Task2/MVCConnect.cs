using Model;
using Task2.Controller;

namespace Task2
{
    abstract class MVCConnect
    {
        public static UnitOfWork UnitOfWork = new UnitOfWork();
        public static string Path { get; set; }
        public InputController InputController = new InputController();
    }
}
