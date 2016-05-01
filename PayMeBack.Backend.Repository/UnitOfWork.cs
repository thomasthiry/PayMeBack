//using PayMeBack.Backend.Models;
//using PayMeBack.Backend.Repository;
//using System;

//namespace PayMeBack.Backend.Contracts
//{
//    public class UnitOfWork : IDisposable
//    {
//        protected PayMeBackContext context = new PayMeBackContext();
//        protected IGenericRepository<Split> _splitRepository;

//        public virtual IGenericRepository<Split> SplitRepository
//        {
//            get
//            {
//                if (_splitRepository == null)
//                {
//                    _splitRepository = new GenericRepository<Split>(context);
//                }
//                return _splitRepository;
//            }
//        }

//        public void Save()
//        {
//            context.SaveChanges();
//        }

//        private bool disposed = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposed)
//            {
//                if (disposing)
//                {
//                    context.Dispose();
//                }
//            }
//            disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}