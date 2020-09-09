using Model;
using Task2.Controller;

namespace Task2
{
    abstract class MVCConnect
    {
        public MVCConnect() { }
        public MVCConnect(MVCConnect core)
        {
            unitOfWork = core.UnitOfWork;
            view = core.View;
        }
        protected readonly UnitOfWork unitOfWork = new UnitOfWork();
        protected readonly View view = new View();
        public InputController inputController = new InputController();
        public UnitOfWork UnitOfWork => unitOfWork;
        public View View => view;

    }
}
