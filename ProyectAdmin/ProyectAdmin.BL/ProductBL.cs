using ProyectAdmin.BL.DTOs.ProductDTOs;
using ProyectAdmin.BL.Interfaces;
using ProyectAdmin.EN;
using ProyectAdmin.EN.Interfaces;

namespace ProyectAdmin.BL
{
    public class ProductBL : IProductBL
    {
        readonly IProduct _productDAL;
        readonly IUnitOfWork _unitWork;

        public ProductBL(IProduct productDAL, IUnitOfWork unitOfWork)
        {
            _productDAL = productDAL;
            _unitWork = unitOfWork;
        }

        public async Task<ProductCreateOutputDTO> CreateProduct(ProductCreateInputDTO pProductos)
        {
            {

                Product newProduct = new Product()
                {
                    NameProduct = pProductos.NameProduct,
                    Quantity = pProductos.Quantity,
                    Dimensions = pProductos.Dimensions,
                    Description = pProductos.Description,
                    AcquisitionDate = pProductos.AcquisitionDate,
                    DueDate = pProductos.DueDate,
                    Price = pProductos.Price,
                    ImageUrl = pProductos.ImageUrl
                };

                Product existingProduct = await _productDAL.GetByName(newProduct);


                if (existingProduct != null)
                {
                    throw new ArgumentException("Ya existe un producto con este nombre.");
                }


                _productDAL.Create(newProduct);
                await _unitWork.SaveChangesAsync();
                ProductCreateOutputDTO productsOutput = new ProductCreateOutputDTO()
                {
                    IdProduct = newProduct.IdProduct,
                    NameProduct = newProduct.NameProduct,
                    Quantity = newProduct.Quantity,
                    Dimensions = newProduct.Dimensions,
                    Description = newProduct.Description,
                    AcquisitionDate = newProduct.AcquisitionDate,
                    DueDate = newProduct.DueDate,
                     Price = newProduct.Price,
                    ImageUrl = newProduct.ImageUrl
                };

                return productsOutput;
            }
        }

        public async Task<ProductDeleteDTO> DeleteProduct(ProductDeleteDTO pProductos)
        {
            Product isProduct = await _productDAL.GetOne(pProductos.IdProduct);

            if (isProduct.IdProduct == pProductos.IdProduct)
            {
                _productDAL.Delete(isProduct);
                await _unitWork.SaveChangesAsync();
                ProductDeleteDTO status = new ProductDeleteDTO()
                {
                    IsDeleted = true
                };

                return status;
            }
            throw new Exception($"Producto con {pProductos.IdProduct} no encontrado");
        }

        public async Task<List<ProductSearchOutputDTO>> Search(ProductSearchOutputDTO pProductos)
        {
            List<Product> products = await _productDAL.Get(new Product
            {
                IdProduct = pProductos.IdProduct,
                NameProduct = pProductos.NameProduct,
                Quantity = pProductos.Quantity,
                Dimensions = pProductos.Dimensions,
                Description = pProductos.Description,
                AcquisitionDate = pProductos.AcquisitionDate,
                DueDate = pProductos.DueDate,
                Price = pProductos.Price,
                ImageUrl = pProductos.ImageUrl

            });

            // Agrupar por BodegaId
            var groupedProducts = products.GroupBy(p => p.IdProduct);

            List<ProductSearchOutputDTO> result = new List<ProductSearchOutputDTO>();

            foreach (var group in groupedProducts)
            {
                foreach (var product in group)
                {
                    result.Add(new ProductSearchOutputDTO
                    {
                        IdProduct = product.IdProduct,
                        NameProduct = product.NameProduct,
                        Quantity = product.Quantity,
                        Dimensions = product.Dimensions,
                        Description = product.Description,
                        AcquisitionDate = product.AcquisitionDate,
                        DueDate = product.DueDate,
                          Price = product.Price,
                        ImageUrl = product.ImageUrl
                    });
                }
            }

            return result;
        }

        public async Task<ProductGellAllDTO> SearchOne(ProductGetByIdDTO pProductos)
        {
            Product byProduct = new Product()
            {
                IdProduct = pProductos.IdProduct
            };
            Product isProduct = await _productDAL.GetOne(byProduct.IdProduct);

            if (isProduct != null)
            {

                ProductGellAllDTO products = new ProductGellAllDTO()
                {
                    IdProduct = isProduct.IdProduct,
                    NameProduct = isProduct.NameProduct,
                    Quantity = isProduct.Quantity,
                    Dimensions = isProduct.Dimensions,
                    Description = isProduct.Description,
                    AcquisitionDate = isProduct.AcquisitionDate,
                    DueDate = isProduct.DueDate,
                    Price = isProduct.Price,
                    ImageUrl = isProduct.ImageUrl

                };
                return products;
            }
            throw new Exception($"Producto con Id: {pProductos.IdProduct} no encontrado");
        
    }


        public async Task UpdateProduct(ProductUpdateDTO pProductos)
        {
            Product productUpdate = await _productDAL.GetOne(pProductos.IdProduct);

            if (productUpdate.IdProduct == pProductos.IdProduct)
            {
                productUpdate.NameProduct = pProductos.NameProduct;
                productUpdate.Quantity = pProductos.Quantity;
                productUpdate.Dimensions = pProductos.Dimensions;
                productUpdate.Description = pProductos.Description;
                productUpdate.AcquisitionDate = pProductos.AcquisitionDate;
                productUpdate.DueDate = pProductos.DueDate;
                productUpdate.Price = pProductos.Price;

                _productDAL.Update(productUpdate);
                await _unitWork.SaveChangesAsync();
                // No necesitas retornar ningún valor aquí
                return;
            }

            throw new Exception($"El producto con el Id: {pProductos.IdProduct} no fue encontrado");
        }

    }
}
