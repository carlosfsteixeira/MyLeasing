using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Owner ToOwner(OwnerViewModel model, string path, bool isNew)
        {
            return new Owner
            {
                Id = isNew ? 0 : model.Id,
                Image = path,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Adress = model.Adress,
                User = model.User,
            };
        }

        public Lessee ToLessee(LesseeViewModel model, string path, bool isNew)
        {
            return new Lessee
            {
                Id = isNew ? 0 : model.Id,
                Image = path,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Adress = model.Adress,
                User = model.User,
            };
        }

        public OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                Image = owner.Image,
                Document = owner.Document,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Adress = owner.Adress,
                User = owner.User,
            };
        }

        public LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel
            {
                Id = lessee.Id,
                Image = lessee.Image,
                Document = lessee.Document,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                Adress = lessee.Adress,
                User = lessee.User,
            };
        }
    }
}
