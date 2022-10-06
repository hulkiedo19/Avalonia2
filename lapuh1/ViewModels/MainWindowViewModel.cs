using lapuh1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lapuh1.ViewModels
{
    public class ListUnit
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Article { get; set; }
        public string Materials { get; set; } = null;
        public string Image { get; set; }
        public decimal Cost { get; set; }

        public ListUnit(string title, string type, string article, string image, decimal cost)
        {
            this.Title = title;
            this.Type = type;
            this.Article = article;
            this.Image = image;
            this.Cost = cost;
        }
    }
    public class MainWindowViewModel : ViewModelBase
    {
        private List<ListUnit> list = new List<ListUnit>();

        private List<Material> GetProductIdMaterials(int product_id)
        {
            List<ProductMaterial> productMaterials = new db_1Context().ProductMaterials
                .Include(m => m.Material)
                .Where(m_id => m_id.ProductId == product_id)
                .ToList();

            List<Material> product_id_materials = new List<Material>();

            for (int i = 0; i < productMaterials.Count; i++)
            {
                product_id_materials.Add(productMaterials[i].Material);
            }

            return product_id_materials;
        }

        private string MaterialsToString(List<Material> materials)
        {
            if (materials.Count == 0)
                return null;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < materials.Count; i++)
            {
                sb.Append($"{materials[i].Title} ");
            }

            return sb.ToString();
        }

        public MainWindowViewModel()
        {
            List<Product> products = new db_1Context().Products
                .Include(p => p.ProductType)
                .ToList();

            for (int i = 0; i < products.Count; i++)
            {
                ListUnit listUnit = new ListUnit(products[i].Title, products[i].ProductType.Title, products[i].ArticleNumber, products[i].Image, products[i].MinCostForAgent);

                List<Material> materials = GetProductIdMaterials(products[i].Id);

                listUnit.Materials = MaterialsToString(materials);

                list.Add(listUnit);
            }
        }

        public List<ListUnit> ListUnits
        {
            get => list;
        }

        /*private List<ProductMaterial> product_material;

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
        }*/
    }
}
