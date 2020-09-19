using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyCollection.Web.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Codigo")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Code { get; set; }

        [Display(Name = "Codigo de barras")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Barcode { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Unidad de Compra")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string PurchaseUnit { get; set; }

        [Display(Name = "Venta")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Sale { get; set; }
        
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Factor { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public decimal IVA { get; set; }

        [Display(Name = "Ubicación")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Location { get; set; }

        [Display(Name = "Notas")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Remarks { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Precio 2")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price2 { get; set; }

        [Display(Name = "Precio 3")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price3 { get; set; }

        [Display(Name = "Precio 4")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price4 { get; set; }

        [Display(Name = "Precio 5")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price5 { get; set; }

        [Display(Name = "Punto de Reorden")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public decimal ReorderPoint { get; set; }

        [Display(Name = "Ultimo Costo")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal LastCost { get; set; }

        [Display(Name = "Está disponible ?")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Linea")]
        public Line Line { get; set; }

        [Display(Name = "Sublinea")]
        public Subline Subline { get; set; }

        [Display(Name = "Proveedor")]
        public Provider Provider { get; set; }

        public ICollection<Inventory> Inventories { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<OrderDetailTmp> OrderDetailTmps { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        public ICollection<SaleDetailTmp> SaleDetailTmps { get; set; }

        public string FirstImage => ProductImages == null || ProductImages.Count == 0
            ? "https://mycollectionweb.azurewebsites.net/images/ProductImages/noImage.png"
            : ProductImages.FirstOrDefault().ImageUrl;
    }
}
