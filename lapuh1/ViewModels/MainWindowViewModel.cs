using lapuh1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lapuh1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<ProductMaterial> product_material;

        public MainWindowViewModel()
        {
            product_material = new db_1Context().ProductMaterials
                .Include(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .Include(m => m.Material)
                .ToList();
        }

        public List<ProductMaterial> ProductMaterial
        {
            get => product_material;
        }
    }
}
