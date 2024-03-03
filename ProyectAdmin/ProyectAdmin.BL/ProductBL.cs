using Firebase.Auth;
using Firebase.Storage;
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

        public async Task<string> SubirArchivo(Stream archivo, string nombre)
        {
            //Este código se utiliza para autenticar un usuario en Firebase utilizando
            //su dirección de correo electrónico y contraseña, y luego subir un archivo a Firebase Storage. 

            //email y clave contienen la dirección de correo electrónico y la contraseña del usuario
            //que se utilizarán para iniciar sesión en Firebase.	
            string email = "mauritaprueba@gmail.com";
            string clave = "Maurita123";
            //ruta contiene la dirección del almacenamiento de Firebase donde se desea cargar el archivo.		
            string ruta = "mauritafirebase.appspot.com";
            //api_key es la clave de API necesaria para autenticarse con Firebase.
            string api_key = "AIzaSyAtvaAHfOrWdjrEIb8vmAYfLP1kP1D8tkM";


            //Se crea una instancia de FirebaseAuthProvider utilizando la clave de API proporcionada.
            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            //Se utiliza auth.SignInWithEmailAndPasswordAsync(email, clave) para autenticar al usuario
            //utilizando su dirección de correo electrónico y contraseña.
            //El resultado de esta autenticación se almacena en la variable a.
            var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();

            //Se crea una instancia de FirebaseStorage para interactuar con Firebase Storage.
            //Se configura con la ruta de almacenamiento y se proporciona un token de autenticación
            //en AuthTokenAsyncFactory, que se obtiene del objeto de autenticación a.
            var task = new FirebaseStorage(
                ruta,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                //Se especifica una ubicación dentro del almacenamiento de Firebase donde se desea cargar el archivo.
                //En este caso, se utiliza la ruta "Fotos_Peliculas" y el nombre del archivo
                //se espera que esté en la variable nombre.
                .Child("Fotos_Pastel")
                .Child(nombre)

                //Se utiliza PutAsync para cargar el archivo especificado (archivo) a la ubicación de
                //Firebase Storage que se ha configurado. También se utiliza un token de cancelación para
                //permitir la cancelación de la operación si es necesario.
                .PutAsync(archivo, cancellation.Token);


            //Se espera a que se complete la carga del archivo y se almacena la URL de descarga en la variable downloadURL.
            var downloadURL = await task;

            //Finalmente, la URL de descarga se devuelve como resultado de la función.
            return downloadURL;

        }
    }
}
