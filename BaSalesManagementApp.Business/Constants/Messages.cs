using Microsoft.AspNetCore.Http;

namespace BaSalesManagementApp.Business.Constants
{
    public class Messages
    {       
        public const string STUDENT_LISTED_SUCCESS = "Öğrenci Listeleme başarılı.";
        public const string STUDENT_ADDED_SUCCESS = "Öğrenci Ekleme başarılı.";
        public const string STUDENT_DELETED_SUCCESS = "Öğrenci Silme başarılı.";
        public const string STUDENT_BROUGHT_SUCCESS = "Öğrenci Getirme başarılı.";

        #region Promotion Messages

        public const string PROMOTION_CREATE_ERROR = "PROMOTION_CREATE_ERROR";
        public const string PROMOTION_CREATE_SUCCESS = "PROMOTION_CREATE_SUCCESS";

        public const string PROMOTION_DELETE_ERROR = "PROMOTION_DELETE_ERROR";
        public const string PROMOTION_DELETE_SUCCESS = "PROMOTION_DELETE_SUCCESS";
        public const string PROMOTION_NOT_FOUND = "PROMOTION_NOT_FOUND";

        public const string PROMOTION_LISTED_ERROR = "PROMOTION_LISTED_ERROR";
        public const string PROMOTION_LISTED_SUCCESS = "PROMOTION_LISTED_SUCCESS";
        public const string PROMOTION_LISTED_NOTFOUND = "PROMOTION_LISTED_NOTFOUND";
        public const string PROMOTION_LISTED_EMPTY = "PROMOTION_LISTED_EMPTY";

        public const string PROMOTION_GETBYID_ERROR = "PROMOTION_GETBYID_ERROR";
        public const string PROMOTION_GETBYID_SUCCESS = "PROMOTION_GETBYID_SUCCESS";

        public const string PROMOTION_UPDATE_ERROR = "PROMOTION_UPDATE_ERROR";
        public const string PROMOTION_UPDATE_SUCCESS = "PROMOTION_UPDATE_SUCCESS";

        // Promotion - Product / Company  => Relation Messages
        public const string PROMOTION_PRODUCT_RELATION = "PROMOTION_PRODUCT_RELATION";
        public const string PROMOTION_COMPANY_RELATION = "PROMOTION_COMPANY_RELATION";
        public const string PROMOTION_PRODUCT_DELETED = "PROMOTION_PRODUCT_DELETED";
        public const string PROMOTION_COMPANY_DELETED = "PROMOTION_COMPANY_DELETED";

        // Promotion validation messages
        public const string PROMOTION_DISCOUNT_REQUIRED = "PROMOTION_DISCOUNT_REQUIRED";
        public const string PROMOTION_DISCOUNT_GREATER_THAN_ZERO = "PROMOTION_DISCOUNT_GREATER_THAN_ZERO";
        public const string PROMOTION_DISCOUNT_LESS_THAN_HUNDRED = "PROMOTION_DISCOUNT_LESS_THAN_HUNDRED";
        public const string PROMOTION_PRICE_REQUIRED = "PROMOTION_PRICE_REQUIRED";
        public const string PROMOTION_PRICE_GREATER_THAN_ZERO = "PROMOTION_PRICE_GREATER_THAN_ZERO";
        public const string PROMOTION_TOTAL_PRICE_REQUIRED = "PROMOTION_TOTAL_PRICE_REQUIRED";
        public const string PROMOTION_TOTAL_PRICE_GREATER_THAN_ZERO = "PROMOTION_TOTAL_PRICE_GREATER_THAN_ZERO";
        public const string PROMOTION_TOTAL_PRICE_GREATER_THAN_OR_EQUAL_PRICE = "PROMOTION_TOTAL_PRICE_GREATER_THAN_OR_EQUAL_PRICE";
        public const string PROMOTION_IS_ACTIVE_REQUIRED = "PROMOTION_IS_ACTIVE_REQUIRED";
        public const string PROMOTION_START_DATE_REQUIRED = "PROMOTION_START_DATE_REQUIRED";
        public const string PROMOTION_END_DATE_REQUIRED = "PROMOTION_END_DATE_REQUIRED";
        public const string PROMOTION_START_DATE_GREATER_THAN_TODAY = "PROMOTION_START_DATE_GREATER_THAN_TODAY";
        public const string PROMOTION_START_DATE_LESS_THAN_END_DATE = "PROMOTION_START_DATE_LESS_THAN_END_DATE";
        public const string PROMOTION_END_DATE_GREATER_THAN_START_DATE = "PROMOTION_END_DATE_GREATER_THAN_START_DATE";
        public const string PROMOTION_START_DATE_NOT_EQUAL_END_DATE = "PROMOTION_START_DATE_NOT_EQUAL_END_DATE";
        #endregion


        // Branch CRUD İşlemleri ile ilgili mesajlar

        public const string BRANCH_ADD_ERROR = "BRANCH_ADD_ERROR"; 
        public const string BRANCH_ADD_SUCCESS = "BRANCH_ADD_SUCCESS";

        public const string BRANCH_DELETE_ERROR = "BRANCH_DELETE_ERROR";
        public const string BRANCH_DELETE_SUCCESS = "BRANCH_DELETE_SUCCESS";

        public const string BRANCH_LISTED_ERROR = "BRANCH_LISTED_ERROR";
        public const string BRANCH_LISTED_SUCCESS = "BRANCH_LISTED_SUCCESS";
        public const string BRANCH_LISTED_NOTFOUND = "BRANCH_LISTED_NOTFOUND";

        public const string BRANCH_GETBYID_ERROR = "BRANCH_GETBYID_ERROR";
        public const string BRANCH_GETBYID_SUCCESS = "BRANCH_GETBYID_SUCCESS";

        public const string BRANCH_UPDATE_ERROR = "BRANCH_UPDATE_ERROR";
        public const string BRANCH_UPDATE_SUCCESS = "BRANCH_UPDATE_SUCCESS";        

        public const string BRANCH_NAME_MAXIMUM_LENGTH = "BRANCH_NAME_MAXIMUM_LENGTH";
        public const string BRANCH_NAME_MINIMUM_LENGTH = "BRANCH_NAME_MINIMUM_LENGTH";
        public const string BRANCH_NAME_NOT_EMPTY = "BRANCH_NAME_NOT_EMPTY";

        public const string BRANCH_ADDRESS_MAXIMUM_LENGTH = "BRANCH_ADDRESS_MAXIMUM_LENGTH";
        public const string BRANCH_ADDRESS_MINIMUM_LENGTH = "BRANCH_ADDRESS_MINIMUM_LENGTH";
        public const string BRANCH_ADDRESS_NOT_EMPTY = "BRANCH_ADDRESS_NOT_EMPTY";

        public const string BRANCH_COMPANY_RELATION = "BRANCH_COMPANY_RELATION";

        public const string BRANCH_COMPANY_LISTED_DELETED = "BRANCH_COMPANY_LISTED_DELETED";

        #region Order Messages and Validation Messages

        public const string ORDER_CREATED_SUCCESS = "ORDER_CREATED_SUCCESS";
        public const string ORDER_CREATE_FAILED = "ORDER_CREATE_FAILED";
        public const string ORDER_DELETED_SUCCESS = "ORDER_DELETED_SUCCESS";
        public const string ORDER_DELETE_FAILED = "ORDER_DELETE_FAILED";
        public const string ORDER_LISTED_SUCCESS = "ORDER_LISTED_SUCCESS";
        public const string ORDER_LIST_FAILED = "ORDER_LIST_FAILED";
        public const string ORDER_LIST_EMPTY = "ORDER_LIST_EMPTY";
        public const string ORDER_FOUND_SUCCESS = "ORDER_FOUND_SUCCESS";
        public const string ORDER_NOT_FOUND = "ORDER_NOT_FOUND";
        public const string ORDER_GET_FAILED = "ORDER_GET_FAILED";
        public const string ORDER_UPDATE_SUCCES = "ORDER_UPDATE_SUCCES";
        public const string ORDER_UPDATE_FAILED = "ORDER_UPDATE_FAILED";
        public const string ORDER_EMPTY_VALIDATION = "ORDER_EMPTY_VALIDATION";
        public const string ORDER_TOTALPRICE_VALIDATION = "ORDER_TOTALPRICE_VALIDATION";
        public const string ORDER_QUANTITY_VALIDATION = "ORDER_QUANTITY_VALIDATION";
        public const string ORDER_DATE_VALIDATION = "ORDER_DATE_VALIDATION";
        public const string ORDER_PRECISION_VALIDATION = "ORDER_PRECISION_VALIDATION";
        public const string ORDER_WHOLE_NUMBER_VALIDATION = "ORDER_WHOLE_NUMBER_VALIDATION";
        public const string ORDER_DISCOUNT_VALIDATION = "ORDER_DISCOUNT_VALIDATION";



        #endregion

        #region CATEGORY Messages
        // Category messages
        public const string CATEGORY_CREATED_SUCCESS = "CATEGORY_CREATED_SUCCESS";
        public const string CATEGORY_DELETED_SUCCESS = "CATEGORY_DELETED_SUCCESS";
        public const string CATEGORY_FOUND_SUCCESS = "CATEGORY_FOUND_SUCCESS";
        public const string CATEGORY_LISTED_SUCCESS = "CATEGORY_LISTED_SUCCESS";
        public const string CATEGORY_UPDATED_SUCCESS = "CATEGORY_UPDATED_SUCCESS";

        public const string CATEGORY_NOT_FOUND = "CATEGORY_NOT_FOUND";
        public const string CATEGORY_CREATE_FAILED = "CATEGORY_CREATE_FAILED";
        public const string CATEGORY_DELETE_FAILED = "CATEGORY_DELETE_FAILED";
        public const string CATEGORY_LIST_EMPTY = "CATEGORY_LIST_EMPTY";
        public const string CATEGORY_LIST_FAILED = "CATEGORY_LIST_FAILED";
        public const string CATEGORY_GET_FAILED = "CATEGORY_GET_FAILED";
        public const string CATEGORY_UPDATED_FAILED = "CATEGORY_UPDATED_FAILED";

        //Category Validations
        public const string CATEGORY_NAMESPACE_IS_REQUIRED = "CATEGORY_NAMESPACE_IS_REQUIRED";
        public const string CATEGORY_MAX_LENGTH = "CATEGORY_MAX_LENGTH";
        public const string CATEGORY_MIN_LENGTH = "CATEGORY_MIN_LENGTH";
        #endregion




        //COMPANY CRUD İŞLEMLERİ İLE İLGİLİ MESAJLAR
        #region COMPANY 
        public const string COMPANY_ADD_ERROR = "COMPANY_ADD_ERROR";
        public const string COMPANY_ADD_SUCCESS = "COMPANY_ADD_SUCCESS";

        public const string COMPANY_DELETE_ERROR = "COMPANY_DELETE_ERROR";
        public const string COMPANY_DELETE_SUCCESS = "COMPANY_DELETE_SUCCESS";

        public const string COMPANY_LISTED_ERROR = "COMPANY_LISTED_ERROR";
        public const string COMPANY_LISTED_SUCCESS = "COMPANY_LISTED_SUCCESS";
        public const string COMPANY_LISTED_NOTFOUND = "COMPANY_LISTED_NOTFOUND";

        public const string COMPANY_GETBYID_ERROR = "COMPANY_GETBYID_ERROR";
        public const string COMPANY_GETBYID_SUCCESS = "COMPANY_GETBYID_SUCCESS";

        public const string COMPANY_UPDATE_ERROR = "COMPANY_UPDATE_ERROR";
        public const string COMPANY_UPDATE_SUCCESS = "COMPANY_UPDATE_SUCCESS";

        public const string COMPANY_NAME_MAXIMUM_LENGTH = "COMPANY_NAME_MAXIMUM_LENGTH";
        public const string COMPANY_NAME_MINIMUM_LENGTH = "COMPANY_NAME_MINIMUM_LENGTH";
        public const string COMPANY_NAME_NOT_EMPTY = "COMPANY_NAME_NOT_EMPTY";

        public const string COMPANY_ADDRESS_MAXIMUM_LENGTH = "COMPANY_ADDRESS_MAXIMUM_LENGTH";
        public const string COMPANY_ADDRESS_MINIMUM_LENGTH = "COMPANY_ADDRESS_MINIMUM_LENGTH";
        public const string COMPANY_ADDRESS_NOT_EMPTY = "COMPANY_ADDRESS_NOT_EMPTY";

        public const string COMPANY_PHONE_MAXIMUM_LENGTH = "COMPANY_PHONE_MAXIMUM_LENGTH";
        public const string COMPANY_PHONE_MINIMUM_LENGTH = "COMPANY_PHONE_MINIMUM_LENGTH";
        public const string COMPANY_PHONE_NOT_EMPTY = "COMPANY_PHONE_NOT_EMPTY";
        public const string COMPANY_MATCHES = "COMPANY_MATCHES";
        #endregion

        //ProductType Crud İşlemleri İle İlgili Mesajlar

        public const string PRODUCTTYPE_ADD_UNSUCCESS = "PRODUCTTYPE_ADD_UNSUCCESS";
        public const string PRODUCTTYPE_ADD_SUCCESS = "PRODUCTTYPE_ADD_SUCCESS";

        public const string PRODUCTTYPE_DELETE_UNSUCCESS = "PRODUCTTYPE_DELETE_UNSUCCESS";
        public const string PRODUCTTYPE_DELETE_SUCCESS = "PRODUCTTYPE_DELETE_SUCCESS";

        public const string PRODUCTTYPE_UPDATE_UNSUCCESS = "PRODUCTTYPE_UPDATE_UNSUCCESS";
        public const string PRODUCTTYPE_UPDATE_SUCCESS = "PRODUCTTYPE_UPDATE_SUCCESS";

        public const string PRODUCTTYPE_GETBYID_UNSUCCESS = "PRODUCTTYPE_GETBYID_UNSUCCESS";
        public const string PRODUCTTYPE_GETBYID_SUCCESS = "PRODUCTTYPE_GETBYID_SUCCESS";

        public const string PRODUCTTYPE_LISTED_UNSUCCESS = "PRODUCTTYPE_LISTED_UNSUCCESS";
        public const string PRODUCTTYPE_LISTED_SUCCESS = "PRODUCTTYPE_LISTED_SUCCESS";
        public const string PRODUCTTYPE_LISTED_NOTFOUND = "PRODUCTTYPE_LISTED_NOTFOUND";

        public const string PRODUCTTYPE_NAME_MAXIMUM_LENGTH = "PRODUCTTYPE_NAME_MAXIMUM_LENGTH";
        public const string PRODUCTTYPE_NAME_MINIMUM_LENGTH = "PRODUCTTYPE_NAME_MINIMUM_LENGTH";
        public const string PRODUCTTYPE_NAME_NOT_EMPTY = "PRODUCTTYPE_NAME_NOT_EMPTY";

        public const string PRODUCTTYPE_DESCRIPTION_MAXIMUM_LENGTH = "PRODUCTTYPE_DESCRIPTION_MAXIMUM_LENGTH";
        public const string PRODUCTTYPE_DESCRIPTION_MINIMUM_LENGTH = "PRODUCTTYPE_DESCRIPTION_MINIMUM_LENGTH";
        public const string PRODUCTTYPE_DESCRIPTION_NOT_EMPTY = "PRODUCTTYPE_DESCRIPTION_NOT_EMPTY";


        #region Product Messages
        public const string PRODUCT_CREATED_SUCCESS = "PRODUCT_CREATED_SUCCESS";
        public const string PRODUCT_CREATED_ERROR = "PRODUCT_CREATED_ERROR";

        public const string PRODUCT_LISTED_SUCCESS = "PRODUCT_LISTED_SUCCESS";
        public const string PRODUCT_LISTED_ERROR = "PRODUCT_LISTED_ERROR";
        public const string PRODUCT_LISTED_EMPTY = "PRODUCT_LISTED_EMPTY";

        public const string PRODUCT_UPDATED_SUCCESS = "PRODUCT_UPDATED_SUCCESS";
        public const string PRODUCT_UPDATED_ERROR = "PRODUCT_UPDATED_ERROR";


        public const string PRODUCT_DELETED_SUCCESS = "PRODUCT_DELETED_SUCCESS";
        public const string PRODUCT_DELETED_ERROR = "PRODUCT_DELETED_ERROR";

        public const string PRODUCT_CANNOT_DELETE_IS_IN_ORDER = "PRODUCT_CANNOT_DELETE_IS_IN_ORDER";
        public const string PASSIVE_PRODUCT = "PASSIVE_PRODUCT";
        public const string PRODUCT_PASSIVED_SUCCESS = "PRODUCT_PASSIVED_SUCCESS";

        public const string PRODUCT_GET_SUCCESS = "PRODUCT_GET_SUCCESS";
        public const string PRODUCT_NOT_FOUND = "PRODUCT_NOT_FOUND";
        //Product Validasyon Messages 
        public const string PRODUCT_NAME_CANNOT_BE_EMPTY = "PRODUCT_NAME_CANNOT_BE_EMPTY";
        public const string PRODUCT_NAME_CANNOT_CONTAIN_NUMBER = "PRODUCT_NAME_CANNOT_CONTAIN_NUMBER";
        public const string PRODUCT_PRICE_MUST_BE_GREATER_THAN_ZERO = "PRODUCT_PRICE_MUST_BE_GREATER_THAN_ZERO";
        public const string PRODUCT_PRICE_MUST_HAVE_NO_MORE_THAN_TWO_DECIMAL_PLACES = "PRODUCT_PRICE_MUST_HAVE_NO_MORE_THAN_TWO_DECIMAL_PLACES";
        public const string PRODUCT_PRICE_CANNOT_BE_EMPTY = "PRODUCT_PRICE_CANNOT_BE_EMPTY";

        //Product - Company Relation
        public const string PRODUCT_COMPANY_RELATION = "PRODUCT_COMPANY_RELATION";
        public const string PRODUCT_COMPANY_DELETED = "PRODUCT_COMPANY_DELETED";




        //PRODUCT VALİDATİONS

        #endregion


        #region Werehouse Messages
        //Başarılı işlemler için mesajlar
        public const string Warehouse_CREATED_SUCCESS = "Warehouse_CREATED_SUCCESS";
        public const string Warehouse_DELETED_SUCCESS = "Warehouse_DELETED_SUCCESS";
        public const string Warehouse_FOUND_SUCCESS = "Warehouse_FOUND_SUCCESS";
        public const string Warehouse_LISTED_SUCCESS = "Warehouse_LISTED_SUCCESS";
        public const string Warehouse_UPDATED_SUCCESS = "Warehouse_UPDATED_SUCCESS";

        // Hata mesajları
        public const string Warehouse_NOT_FOUND = "Warehouse_NOT_FOUND";
        public const string Warehouse_CREATE_FAILED = "Warehouse_CREATE_FAILED";
        public const string Warehouse_DELETE_FAILED = "Warehouse_DELETE_FAILED";
        public const string Warehouse_LIST_EMPTY = "Warehouse_LIST_EMPTY";
        public const string Warehouse_LIST_FAILED = "Warehouse_LIST_FAILED";
        public const string Warehouse_GET_FAILED = "Warehouse_GET_FAILED";
        public const string Warehouse_UPDATE_FAILED = "Warehouse_UPDATE_FAILED";
        public const string Warehouse_NOT_EMPTY = "Warehouse_NOT_EMPTY";
        public const string Warehouse_MAX_LENGTH = "Warehouse_MAX_LENGTH";
        public const string Warehouse_MIN_LENGTH = "Warehouse_MIN_LENGTH";


        #endregion



        #region Stock Messages

        public const string STOCK_CREATED_SUCCESS = "STOCK_CREATED_SUCCESS";
        public const string STOCK_CREATE_FAILED = "STOCK_CREATE_FAILED";

        public const string STOCK_DELETED_SUCCESS = "STOCK_DELETED_SUCCESS";
        public const string STOCK_DELETE_FAILED = "STOCK_DELETE_FAILED";

        public const string STOCK_LISTED_SUCCESS = "STOCK_LISTED_SUCCESS";
        public const string STOCK_LIST_FAILED = "STOCK_LIST_FAILED";
        public const string STOCK_LIST_EMPTY = "STOCK_LIST_EMPTY";

        public const string STOCK_FOUND_SUCCESS = "STOCK_FOUND_SUCCESS";
        public const string STOCK_NOT_FOUND = "STOCK_NOT_FOUND";
        public const string STOCK_GET_FAILED = "STOCK_GET_FAILED";


        public const string STOCK_UPDATE_SUCCESS = "STOCK_UPDATE_SUCCESS";
        public const string STOCK_UPDATE_FAILED = "STOCK_UPDATE_FAILED";

        public const string STOCK_WHOLENUMBER_VALIDATION = "STOCK_WHOLENUMBER_VALIDATION";
        public const string STOCK_POSITIVE_VALIDATION = "STOCK_POSITIVE_VALIDATION";
        public const string STOCK_EMPTY_VALIDATION = "STOCK_EMPTY_VALIDATION";

        #endregion
        #region StockTypeSize Messages

        public const string STOCK_TYPE_SIZE_CREATED_SUCCESS = "STOCK_TYPE_SIZE_CREATED_SUCCESS";
        public const string STOCK_TYPE_SIZE_CREATE_FAILED = "STOCK_TYPE_SIZE_CREATE_FAILED";

        public const string STOCK_TYPE_SIZE_DELETED_SUCCESS = "STOCK_TYPE_SIZE_DELETED_SUCCESS";
        public const string STOCK_TYPE_SIZE_DELETE_FAILED = "STOCK_TYPE_SIZE_DELETE_FAILED";

        public const string STOCK_TYPE_SIZE_LISTED_SUCCESS = "STOCK_TYPE_SIZE_LISTED_SUCCESS";
        public const string STOCK_TYPE_SIZE_LIST_FAILED = "STOCK_TYPE_SIZE_LIST_FAILED";
        public const string STOCK_TYPE_SIZE_LIST_EMPTY = "STOCK_TYPE_SIZE_LIST_EMPTY";

        public const string STOCK_TYPE_SIZE_FOUND_SUCCESS = "STOCK_TYPE_SIZE_FOUND_SUCCESS";
        public const string STOCK_TYPE_SIZE_NOT_FOUND = "STOCK_TYPE_SIZE_NOT_FOUND";
        public const string STOCK_TYPE_SIZE_GET_FAILED = "STOCK_TYPE_SIZE_GET_FAILED";

        public const string STOCK_TYPE_SIZE_UPDATE_SUCCESS = "STOCK_TYPE_SIZE_UPDATE_SUCCESS";
        public const string STOCK_TYPE_SIZE_UPDATE_FAILED = "STOCK_TYPE_SIZE_UPDATE_FAILED";

        public const string STOCK_TYPE_SIZE_NOT_EMPTY = "STOCK_TYPE_SIZE_NOT_EMPTY";
        public const string STOCK_TYPE_SIZE_MAX_LENGTH = "STOCK_TYPE_SIZE_MAX_LENGTH";
        public const string STOCK_TYPE_SIZE_MIN_LENGTH = "STOCK_TYPE_SIZE_MIN_LENGTH";


        #endregion

        #region EMPLOYEE
        // Employee CRUD İşlemleri ile ilgili mesajlar
        #endregion

        // Employee CRUD İşlemleri ile ilgili mesajlar

        public const string EMPLOYEE_ADD_ERROR = "EMPLOYEE_ADD_ERROR";
		public const string EMPLOYEE_ADD_SUCCESS = "EMPLOYEE_ADD_SUCCESS";

		public const string EMPLOYEE_DELETE_ERROR = "EMPLOYEE_DELETE_ERROR";
        public const string EMPLOYEE_DELETE_SUCCESS = "EMPLOYEE_DELETE_SUCCESS";

		public const string EMPLOYEE_LISTED_ERROR = "EMPLOYEE_LISTED_ERROR";
		public const string EMPLOYEE_LISTED_SUCCESS = "EMPLOYEE_LISTED_SUCCESS";
		public const string EMPLOYEE_LISTED_NOTFOUND = "EMPLOYEE_LISTED_NOTFOUND";

		public const string EMPLOYEE_GETBYID_ERROR = "EMPLOYEE_GETBYID_ERROR";
		public const string EMPLOYEE_GETBYID_SUCCESS = "EMPLOYEE_GETBYID_SUCCESS";

		public const string EMPLOYEE_UPDATE_ERROR = "EMPLOYEE_UPDATE_ERROR";
		public const string EMPLOYEE_UPDATE_SUCCESS = "EMPLOYEE_UPDATE_SUCCESS";

		public const string EMPLOYEE_EMAIL_IN_USE = "EMPLOYEE_EMAIL_IN_USE";

		public const string EMPLOYEE_NAMESPACE_IS_REQUIRED = "EMPLOYEE_NAMESPACE_IS_REQUIRED";
        public const string EMPLOYEE_SURNAME_FIELD_IS_REQUIRED = "EMPLOYEE_SURNAME_FIELD_IS_REQUIRED";
        public const string EMPLOYEE_VALID_EMAIL_REQUIRED = "EMPLOYEE_VALID_EMAIL_REQUIRED";
        public const string EMPLOYEE_INVALID_EMAIL_DOMAIN = "EMPLOYEE_INVALID_EMAIL_DOMAIN";
        public const string EMPLOYEE_NAME_MAX_LENGTH_ERROR = "EMPLOYEE_NAME_MAX_LENGTH_ERROR";
        public const string EMPLOYEE_SURNAME_MAX_LENGTH_ERROR = "EMPLOYEE_SURNAME_MAX_LENGTH_ERROR";
		public const string EMPLOYEE_E_MAIL_FIELD_IS_REQUIRED = "EMPLOYEE_E_MAIL_FIELD_IS_REQUIRED";
		public const string EMPLOYEE_NAME_MIN_LENGTH_ERROR = "EMPLOYEE_NAME_MIN_LENGTH_ERROR";
		public const string EMPLOYEE_SURNAME_MIN_LENGTH_ERROR = "EMPLOYEE_SURNAME_MIN_LENGTH_ERROR";
		public const string EMPLOYEE_NAME_NUMERIC_FORBIDDEN = "EMPLOYEE_NAME_NUMERIC_FORBIDDEN";
		public const string EMPLOYEE_SURNAME_NUMERIC_FORBIDDEN = "EMPLOYEE_SURNAME_NUMERIC_FORBIDDEN";
		public const string EMPLOYEE_PHOTO_SIZE_EXCEEDS_LIMIT = "EMPLOYEE_PHOTO_SIZE_EXCEEDS_LIMIT";
        public const string EMPLOYEE_TITLE_REQUIRED = "EMPLOYEE_TITLE_REQUIRED";
        public const string EMPLOYEE_TURKISH_CHAR = "EMPLOYEE_TURKISH_CHAR";

        #region Admin Messages

        public const string ADMIN_CREATED_SUCCESS = "ADMIN_CREATED_SUCCESS";
        public const string ADMIN_CREATE_ERROR = "ADMIN_CREATE_ERROR";

        public const string ADMIN_DELETED_SUCCESS = "ADMIN_DELETED_SUCCESS";
        public const string ADMIN_DELETE_ERROR = "ADMIN_DELETE_ERROR";

        public const string ADMIN_LISTED_SUCCESS = "ADMIN_LISTED_SUCCESS";
        public const string ADMIN_LISTED_ERROR = "ADMIN_LISTED_ERROR";
        public const string ADMIN_LISTED_NOTFOUND = "ADMIN_LISTED_NOTFOUND";

        public const string ADMIN_GETBYID_ERROR = "ADMIN_GETBYID_ERROR";
        public const string ADMIN_GETBYID_SUCCESS = "ADMIN_GETBYID_SUCCESS";

        public const string ADMIN_UPDATE_ERROR = "ADMIN_UPDATE_ERROR";
        public const string ADMIN_UPDATE_SUCCESS = "ADMIN_UPDATE_SUCCESS";

        public const string ADMIN_EMAIL_IN_USE = "ADMIN_EMAIL_IN_USE";

        public const string ADMIN_NAMESPACE_IS_REQUIRED = "ADMIN_NAMESPACE_IS_REQUIRED";
        public const string ADMIN_SURNAME_FIELD_IS_REQUIRED = "ADMIN_SURNAME_FIELD_IS_REQUIRED";
        public const string ADMIN_VALID_EMAIL_REQUIRED = "ADMIN_VALID_EMAIL_REQUIRED";

        public const string ADMIN_NAME_MAX_LENGTH_ERROR = "ADMIN_NAME_MAX_LENGTH_ERROR";
        public const string ADMIN_SURNAME_MAX_LENGTH_ERROR = "ADMIN_SURNAME_MAX_LENGTH_ERROR";
        public const string ADMIN_E_MAIL_FIELD_IS_REQUIRED = "ADMIN_E_MAIL_FIELD_IS_REQUIRED";

        public const string ADMIN_NAME_MIN_LENGTH_ERROR = "ADMIN_NAME_MIN_LENGTH_ERROR";
        public const string ADMIN_SURNAME_MIN_LENGTH_ERROR = "ADMIN_SURNAME_MIN_LENGTH_ERROR";

        public const string ADMIN_NAME_NUMERIC_FORBIDDEN = "ADMIN_NAME_NUMERIC_FORBIDDEN";
        public const string ADMIN_SURNAME_NUMERIC_FORBIDDEN = "ADMIN_SURNAME_NUMERIC_FORBIDDEN";

        public const string ADMIN_PHOTO_SIZE_EXCEEDS_LIMIT = "ADMIN_PHOTO_SIZE_EXCEEDS_LIMIT";
        public const string ADMIN_INVALID_EMAIL_DOMAIN = "ADMIN_INVALID_EMAIL_DOMAIN";
        public const string ADMIN_TURKISH_CHAR = "ADMIN_TURKISH_CHAR";
        

        #endregion

        #region Recaptcha
        public const string RECAPTCHA_FAILED_ERROR = "RECAPTCHA_FAILED_ERROR";
        #endregion

        #region CHANGEPASSWORD
        public const string CHANGE_PASSWORD_ERROR = "CHANGE_PASSWORD_ERROR";
        public const string CHANGE_PASSWORD_SUCCESS = "CHANGE_PASSWORD_SUCCESS";
        #endregion

        #region CUSTOMER

        public const string CUSTOMER_ADD_ERROR = "CUSTOMER_ADD_ERROR";
        public const string CUSTOMER_ADD_SUCCESS = "CUSTOMER_ADD_SUCCESS";

        public const string CUSTOMER_DELETE_ERROR = "CUSTOMER_DELETE_ERROR";
        public const string CUSTOMER_DELETE_SUCCESS = "CUSTOMER_DELETE_SUCCESS";

        public const string CUSTOMER_FOUND_SUCCESS = "CUSTOMER_FOUND_SUCCESS";
        public const string CUSTOMER_GET_FAILED = "CUSTOMER_GET_FAILED";

        public const string CUSTOMER_LIST_EMPTY = "CUSTOMER_LIST_EMPTY";
        public const string CUSTOMER_LIST_FAILED = "CUSTOMER_LIST_FAILED";

        public const string CUSTOMER_LISTED_ERROR = "CUSTOMER_LISTED_ERROR";
        public const string CUSTOMER_LISTED_SUCCESS = "CUSTOMER_LISTED_SUCCESS";

        public const string CUSTOMER_NOT_FOUND = "CUSTOMER_NOT_FOUND";

        public const string CUSTOMER_UPDATED_FAILED = "CUSTOMER_UPDATED_FAILED";
        public const string CUSTOMER_UPDATED_SUCCESS = "CUSTOMER_UPDATED_SUCCESS";

        //Customer Validasyon Mesajları
        public const string CUSTOMER_NAME_MAXIMUM_LENGTH = "CUSTOMER_NAME_MAXIMUM_LENGTH";
        public const string CUSTOMER_NAME_MINIMUM_LENGTH = "CUSTOMER_NAME_MINIMUM_LENGTH";
        public const string CUSTOMER_NAME_NOT_EMPTY = "CUSTOMER_NAME_NOT_EMPTY";

        public const string CUSTOMER_ADDRESS_MAXIMUM_LENGTH = "CUSTOMER_ADDRESS_MAXIMUM_LENGTH";
        public const string CUSTOMER_ADDRESS_MINIMUM_LENGTH = "CUSTOMER_ADDRESS_MINIMUM_LENGTH";
        public const string CUSTOMER_ADDRESS_NOT_EMPTY = "CUSTOMER_ADDRESS_NOT_EMPTY";

        public const string CUSTOMER_PHONE_MAXIMUM_LENGTH = "CUSTOMER_PHONE_MAXIMUM_LENGTH";
        public const string CUSTOMER_PHONE_MINIMUM_LENGTH = "CUSTOMER_PHONE_MINIMUM_LENGTH";
        public const string CUSTOMER_PHONE_NOT_EMPTY = "CUSTOMER_PHONE_NOT_EMPTY";

        public const string CUSTOMER_MATCHES = "CUSTOMER_MATCHES";
        public const string CUSTOMER_COMPANY_RELATION = "CUSTOMER_COMPANY_RELATION";



        #endregion

        #region Country

        public const string COUNTRY_CREATE_ERROR = "COUNTRY_CREATE_ERROR";
        public const string COUNTRY_CREATE_SUCCESS = "COUNTRY_CREATE_SUCCESS";

        public const string COUNTRY_DELETE_ERROR = "COUNTRY_DELETE_ERROR";
        public const string COUNTRY_DELETE_SUCCESS = "COUNTRY_DELETE_SUCCESS";
        public const string COUNTRY_NOT_FOUND = "COUNTRY_NOT_FOUND";

        public const string COUNTRY_LISTED_ERROR = "COUNTRY_LISTED_ERROR";
        public const string COUNTRY_LISTED_SUCCESS = "COUNTRY_LISTED_SUCCESS";
        public const string COUNTRY_LISTED_NOTFOUND = "COUNTRY_LISTED_NOTFOUND";

        public const string COUNTRY_GETBYID_ERROR = "COUNTRY_GETBYID_ERROR";
        public const string COUNTRY_GETBYID_SUCCESS = "COUNTRY_GETBYID_SUCCESS";

        public const string COUNTRY_UPDATE_ERROR = "COUNTRY_UPDATE_ERROR";
        public const string COUNTRY_UPDATE_SUCCESS = "COUNTRY_UPDATE_SUCCESS";

        public const string COUNTRY_NAME_NOT_EMPTY = "COUNTRY_NAME_NOT_EMPTY";
        public const string COUNTRY_NAME_SHOULD_BE_STRING = "COUNTRY_NAME_SHOULD_BE_STRING";
        public const string COUNTRY_NAME_MUST_BE_UNIQUE = "COUNTRY_NAME_MUST_BE_UNIQUE";
        public const string COUNTRY_NAME_MAXIMUM_LENGTH = "COUNTRY_NAME_MAXIMUM_LENGTH";
        public const string COUNTRY_NAME_MINIMUM_LENGTH = "COUNTRY_NAME_MINIMUM_LENGTH";

         #endregion
        public const string ACCOUNT_ROLE_NOT_FOUND_FOR_USER = "ACCOUNT_ROLE_NOT_FOUND_FOR_USER";
        public const string ACCOUNT_NOT_FOUND = "ACCOUNT_NOT_FOUND";

        public const string PLEASE_ENTER_YOUR_EMAIL = "PLEASE_ENTER_YOUR_EMAIL";
        public const string PLEASE_ENTER_YOUR_EMAIL_WITH_CORRECT_FORMAT = "PLEASE_ENTER_YOUR_EMAIL_WITH_CORRECT_FORMAT";
        public const string PLEASE_ENTER_YOUR_PASSWORD = "PLEASE_ENTER_YOUR_PASSWORD";



        // Silme onaylari
        public const string DELETE_CONFIRM_TITLE_ADMIN = "DELETE_CONFIRM_TITLE_ADMIN";
        public const string DELETE_CONFIRM_ADMIN = "DELETE_CONFIRM_ADMIN";
        public const string DELETE_ADMIN = "DELETE_ADMIN";
        public const string CANCEL_ADMIN = "CANCEL_ADMIN";

        public const string DELETE_CONFIRM_TITLE_BRANCH = "DELETE_CONFIRM_TITLE_BRANCH";
        public const string DELETE_CONFIRM_BRANCH = "DELETE_CONFIRM_BRANCH";
        public const string DELETE_BRANCH = "DELETE_BRANCH";
        public const string CANCEL_BRANCH = "CANCEL_BRANCH";

        public const string DELETE_CONFIRM_TITLE_CATEGORY = "DELETE_CONFIRM_TITLE_CATEGORY";
        public const string DELETE_CONFIRM_CATEGORY = "DELETE_CONFIRM_CATEGORY";
        public const string DELETE_CATEGORY = "DELETE_CATEGORY";
        public const string CANCEL_CATEGORY = "CANCEL_CATEGORY";

        public const string DELETE_CONFIRM_TITLE_COMPANY = "DELETE_CONFIRM_TITLE_COMPANY";
        public const string DELETE_CONFIRM_COMPANY = "DELETE_CONFIRM_COMPANY";
        public const string DELETE_COMPANY = "DELETE_COMPANY";
        public const string CANCEL_COMPANY = "CANCEL_COMPANY";

        public const string DELETE_CONFIRM_TITLE_COUNTRY = "DELETE_CONFIRM_TITLE_COUNTRY";
        public const string DELETE_CONFIRM_COUNTRY = "DELETE_CONFIRM_COUNTRY";
        public const string DELETE_COUNTRY = "DELETE_COUNTRY";
        public const string CANCEL_COUNTRY = "CANCEL_COUNTRY";

        public const string DELETE_CONFIRM_TITLE_CUSTOMER = "DELETE_CONFIRM_TITLE_CUSTOMER";
        public const string DELETE_CONFIRM_CUSTOMER = "DELETE_CONFIRM_CUSTOMER";
        public const string DELETE_CUSTOMER = "DELETE_CUSTOMER";
        public const string CANCEL_CUSTOMER = "CANCEL_CUSTOMER";

        public const string DELETE_CONFIRM_TITLE_EMPLOYEE = "DELETE_CONFIRM_TITLE_EMPLOYEE";
        public const string DELETE_CONFIRM_EMPLOYEE = "DELETE_CONFIRM_EMPLOYEE";
        public const string DELETE_EMPLOYEE = "DELETE_EMPLOYEE";
        public const string CANCEL_EMPLOYEE = "CANCEL_EMPLOYEE";

        public const string DELETE_CONFIRM_TITLE_MANAGER = "DELETE_CONFIRM_TITLE_MANAGER";
        public const string DELETE_CONFIRM_MANAGER = "DELETE_CONFIRM_MANAGER";
        public const string DELETE_MANAGER = "DELETE_MANAGER";
        public const string CANCEL_MANAGER = "CANCEL_MANAGER";

        public const string DELETE_CONFIRM_TITLE_ORDER = "DELETE_CONFIRM_TITLE_ORDER";
        public const string DELETE_CONFIRM_ORDER = "DELETE_CONFIRM_ORDER";
        public const string DELETE_ORDER = "DELETE_ORDER";
        public const string CANCEL_ORDER = "CANCEL_ORDER";

        public const string DELETE_CONFIRM_TITLE_PRODUCT = "DELETE_CONFIRM_TITLE_PRODUCT";
        public const string DELETE_CONFIRM_PRODUCT = "DELETE_CONFIRM_PRODUCT";
        public const string DELETE_PRODUCT = "DELETE_PRODUCT";
        public const string CANCEL_PRODUCT = "CANCEL_PRODUCT";

        public const string DELETE_CONFIRM_TITLE_PRODUCT_TYPE = "DELETE_CONFIRM_TITLE_PRODUCT_TYPE";
        public const string DELETE_CONFIRM_PRODUCT_TYPE = "DELETE_CONFIRM_PRODUCT_TYPE";
        public const string DELETE_PRODUCT_TYPE = "DELETE_PRODUCT_TYPE";
        public const string CANCEL_PRODUCT_TYPE = "CANCEL_PRODUCT_TYPE";

        public const string DELETE_CONFIRM_TITLE_PROMOTION = "DELETE_CONFIRM_TITLE_PROMOTION";
        public const string DELETE_CONFIRM_PROMOTION = "DELETE_CONFIRM_PROMOTION";
        public const string DELETE_PROMOTION = "DELETE_PROMOTION";
        public const string CANCEL_PROMOTION = "CANCEL_PROMOTION";

        public const string DELETE_CONFIRM_TITLE_STOCK = "DELETE_CONFIRM_TITLE_STOCK";
        public const string DELETE_CONFIRM_STOCK = "DELETE_CONFIRM_STOCK";
        public const string DELETE_STOCK = "DELETE_STOCK";
        public const string CANCEL_STOCK = "CANCEL_STOCK";

        public const string DELETE_CONFIRM_TITLE_WAREHOUSE = "DELETE_CONFIRM_TITLE_WAREHOUSE";
        public const string DELETE_CONFIRM_WAREHOUSE = "DELETE_CONFIRM_WAREHOUSE";
        public const string DELETE_WAREHOUSE = "DELETE_WAREHOUSE";
        public const string CANCEL_WAREHOUSE = "CANCEL_WAREHOUSE";

        public const string DELETE_CONFIRM_TITLE_CITY = "DELETE_CONFIRM_TITLE_CITY";
        public const string DELETE_CONFIRM_CITY = "DELETE_CONFIRM_CITY";
        public const string DELETE_CITY = "DELETE_CITY";
        public const string CANCEL_CITY = "CANCEL_CITY";


        #region City Messages
        public const string CITY_CREATED_SUCCESS = "CITY_CREATED_SUCCESS";
        public const string CITY_CREATED_ERROR = "CITY_CREATED_ERROR";

        public const string CITY_LISTED_SUCCESS = "CITY_LISTED_SUCCESS";
        public const string CITY_LISTED_ERROR = "CITY_LISTED_ERROR";
        public const string CITY_LISTED_EMPTY = "CITY_LISTED_EMPTY";

        public const string CITY_UPDATED_SUCCESS = "CITY_UPDATED_SUCCESS";
        public const string CITY_UPDATED_ERROR = "CITY_UPDATED_ERROR";

        public const string CITY_DELETED_SUCCESS = "CITY_DELETED_SUCCESS";
        public const string CITY_DELETED_ERROR = "CITY_DELETED_ERROR";

        public const string CITY_GET_SUCCESS = "CITY_GET_SUCCESS";
        public const string CITY_NOT_FOUND = "CITY_NOT_FOUND";

        public const string CITY_NAME_MUST_BE_UNIQUE = "CITY_NAME_MUST_BE_UNIQUE";




        public const string CITY_NAME_CANNOT_BE_EMPTY = "CITY_NAME_CANNOT_BE_EMPTY";
        public const string CITY_NAME_CANNOT_CONTAIN_NUMBER = "CITY_NAME_CANNOT_CONTAIN_NUMBER";
        #endregion

        public const string MANAGER_CREATED_SUCCESS = "MANAGER_CREATED_SUCCESS";
        public const string MANAGER_CREATE_FAILED = "MANAGER_CREATE_FAILED";

        public const string MANAGER_DELETED_SUCCESS = "MANAGER_DELETED_SUCCESS";
        public const string MANAGER_DELETE_FAILED = "MANAGER_DELETE_FAILED";

        public const string MANAGER_LISTED_SUCCESS = "MANAGER_LISTED_SUCCESS";
        public const string MANAGER_LIST_FAILED = "MANAGER_LIST_FAILED";
        public const string MANAGER_LIST_EMPTY = "MANAGER_LIST_EMPTY";

        public const string MANAGER_FOUND_SUCCESS = "MANAGER_FOUND_SUCCESS";
        public const string MANAGER_NOT_FOUND = "MANAGER_NOT_FOUND";
        public const string MANAGER_GET_FAILED = "MANAGER_GET_FAILED";

        public const string MANAGER_UPDATE_SUCCESS = "MANAGER_UPDATE_SUCCESS";
        public const string MANAGER_UPDATE_FAILED = "MANAGER_UPDATE_FAILED";

        public const string MANAGER_FIRST_NAME_NOT_EMPTY = "MANAGER_FIRST_NAME_NOT_EMPTY";
        public const string MANAGER_FIRST_NAME_MAX_LENGTH = "MANAGER_FIRST_NAME_MAX_LENGTH";
        public const string MANAGER_FIRST_NAME_MIN_LENGTH = "MANAGER_FIRST_NAME_MIN_LENGTH";
        public const string MANAGER_FIRST_NAME_INVALID_CHARS = "MANAGER_FIRST_NAME_INVALID_CHARS";

        public const string MANAGER_LAST_NAME_NOT_EMPTY = "MANAGER_LAST_NAME_NOT_EMPTY";
        public const string MANAGER_LAST_NAME_MAX_LENGTH = "MANAGER_LAST_NAME_MAX_LENGTH";
        public const string MANAGER_LAST_NAME_MIN_LENGTH = "MANAGER_LAST_NAME_MIN_LENGTH";
        public const string MANAGER_LAST_NAME_INVALID_CHARS = "MANAGER_LAST_NAME_INVALID_CHARS";

        public const string MANAGER_EMAIL_NOT_EMPTY = "MANAGER_EMAIL_NOT_EMPTY";
        public const string MANAGER_EMAIL_INVALID = "MANAGER_EMAIL_INVALID";
        public const string IDENTITY_ID_NOT_DETERMINED = "IDENTITY_ID_NOT_DETERMINED";
    }
}
