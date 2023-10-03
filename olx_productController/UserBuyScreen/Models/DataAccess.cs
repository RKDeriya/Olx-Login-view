using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UserBuyScreen.Models
{
    public class DataAccess
    {
        private SqlConnection _connection;
        public DataAccess()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

        }

        #region BYPRICE
        public List<ModelMyAdvertise> GetByPrice(decimal min, decimal max)
        {
            List<ModelMyAdvertise> price = new List<ModelMyAdvertise>();
            string query = "select ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice,ar.areaName,ct.cityName,st.stateName" +
                " from tbl_MyAdvertise ad\r\n" +
                "join tbl_Area ar on ad.areaId=ar.areaId\r\n" +
                "join tbl_City ct on ar.cityId=ct.cityId\r\n" +
                "join tbl_State st on ct.stateId=st.stateId\r\nwhere advertisePrice>=@minprice and advertisePrice<=@maxprice"
                ;
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@minprice", min);
            cmd.Parameters.AddWithValue("@maxprice", max);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ModelMyAdvertise byprice = new ModelMyAdvertise()
                {
                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    advertiseDescription = reader["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                    cityName = reader["cityName"].ToString()

                };
                price.Add(byprice);


            }
            _connection.Close();
            return price;

        }
        #endregion

        #region ByCity
        public List<ModelMyAdvertise> GetProductByCity(int cityId)
        {
            List<ModelMyAdvertise> city = new List<ModelMyAdvertise>();
            string query = "select ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice,ar.areaName,ct.cityName,st.stateName" +
                " from tbl_MyAdvertise ad\r\n" +
                "join tbl_Area ar on ad.areaId=ar.areaId\r\n" +
                "join tbl_City ct on ar.cityId=ct.cityId\r\n" +
                "join tbl_State st on ct.stateId=st.stateId\r\nwhere ct.cityId=@cityid";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            cmd.Parameters.AddWithValue("@cityid", cityId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ModelMyAdvertise bycity = new ModelMyAdvertise()
                {
                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    advertiseDescription = reader["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                    areaName = reader["areaName"].ToString(),
                    stateName = reader["stateName"].ToString(),
                    cityName = reader["cityName"].ToString()
                };
                city.Add(bycity);
            }
            _connection.Close();
            return city;
        }
        #endregion

        #region Bystate
        public List<ModelMyAdvertise> GetProductByState(int stateId)
        {
            List<ModelMyAdvertise> state = new List<ModelMyAdvertise>();
            string query = "select ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice,ar.areaName,ct.cityName,st.stateName" +
                " from tbl_MyAdvertise ad\r\njoin tbl_Area ar on ad.areaId=ar.areaId\r\n" +
                "join tbl_City ct on ar.cityId=ct.cityId\r\njoin tbl_State st on ct.stateId=st.stateId\r\nwhere st.stateId=@stateid";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            cmd.Parameters.AddWithValue("@stateid", stateId);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ModelMyAdvertise modelMyAdvertise = new ModelMyAdvertise()
                {
                    advertiseTitle = dr["advertiseTitle"].ToString(),
                    advertiseDescription = dr["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(dr["advertisePrice"]),
                    stateName = dr["stateName"].ToString(),
                    cityName = dr["cityName"].ToString()
                };

                state.Add(modelMyAdvertise);


            }
            _connection.Close();
            return state;
        }
        #endregion

        #region ShowAllCategory
        public IEnumerable<ModelProductCategory> GetCategory()
        {
            List<ModelProductCategory> products = new List<ModelProductCategory>();
            string query = "SELECT productCategoryId,productCategoryName FROM tbl_ProductCategory";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ModelProductCategory category = new ModelProductCategory()
                {
                    productCategoryId = Convert.ToInt32(dr["productCategoryId"]),
                    productCategoryName = Convert.ToString(dr["productCategoryName"])

                };
                products.Add(category);
            }
            _connection.Close();
            return products;
        }
        #endregion
        #region BySubcategory
        public List<ModelMyAdvertise> GetProductBySubcategory(int productSubCategoryId)
        {
            List<ModelMyAdvertise> subcategory = new List<ModelMyAdvertise>();
            string query = "  select ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice from tbl_MyAdvertise ad" +
                "\r\n  join tbl_ProductSubCategory ps on ad.productSubCategoryId=ps.productSubCategoryId" +
                " \r\n  where ps.productSubCategoryId=@subcategory";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            cmd.Parameters.AddWithValue("@subcategory", productSubCategoryId);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ModelMyAdvertise advertise = new ModelMyAdvertise()
                {
                    advertiseTitle = dr["advertiseTitle"].ToString(),
                    advertiseDescription = dr["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(dr["advertisePrice"])
                };
                subcategory.Add(advertise);
            }
            _connection.Close();
            return subcategory;
        }

        #endregion

        #region ByCategoryById
        public List<ModelMyAdvertise> GetCategoryById(int productCategoryId)
        {
            // SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            List<ModelMyAdvertise> list = new List<ModelMyAdvertise>();
            string query = "select ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice from tbl_MyAdvertise ad " +
                "join tbl_ProductSubCategory ps on ad.productSubCategoryId=ps.productSubCategoryId" +
                " where ps.productCategoryId=@productCategoryId";

            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();
            cmd.Parameters.AddWithValue("@productCategoryId", productCategoryId);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ModelMyAdvertise modelMyAdvertise = new ModelMyAdvertise()
                {

                    advertiseTitle = reader["advertiseTitle"].ToString(),
                    advertiseDescription = reader["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(reader["advertisePrice"])
                };

                list.Add(modelMyAdvertise);
            }
            _connection.Close();
            return list;
        }

        #endregion

        #region ByProduct

        public IEnumerable<ModelAdvertiseImages> GetProducts()
        {
            List<ModelAdvertiseImages> products = new List<ModelAdvertiseImages>();
            string query = "select img.imageData,ad.advertiseTitle,ad.advertiseDescription,ad.advertisePrice from " +
                "tbl_MyAdvertise ad join tbl_AdvertiseImages img on ad.advertiseId=img.advertiseId where " +
                "ad.advertiseId=img.advertiseId ";
            SqlCommand cmd = new SqlCommand(query, _connection);
            _connection.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ModelAdvertiseImages advertise = new ModelAdvertiseImages()
                {
                    // imageData = dr["imageData"].ToString(),

                    //imageData = Convert.ToBase64String((byte[])dr["imageData"]),//updated for image path [dhriti-29/09 6:30]
                    imageData = (byte[])dr["imageData"],
                    advertiseTitle = dr["advertiseTitle"].ToString(),
                    advertiseDescription = dr["advertiseDescription"].ToString(),
                    advertisePrice = Convert.ToInt32(dr["advertisePrice"])
                };
                products.Add(advertise);
            }
            _connection.Close();
            return products;
        }
        #endregion

        #region subcategorydetail
        public List<ModelProductSubCategory> GetSubByCategoryId(int procategoryid)
        {
            List<ModelProductSubCategory> sub = new List<ModelProductSubCategory>();
            SqlCommand cmd = new SqlCommand("subcategorydetails", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productcategoryid", procategoryid);
            _connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ModelProductSubCategory model = new ModelProductSubCategory()
                {
                    productSubCategoryId = Convert.ToInt32(reader["productSubCategoryId"]),
                    productSubCategoryName = reader["productSubCategoryName"].ToString()
                };
                sub.Add(model);
            }
            _connection.Close();
            return sub;
        }

        #endregion
    }
}

