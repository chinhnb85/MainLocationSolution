using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LibCore.Data;
using LibCore.Helper.Logging;
using ModelCMS.Base;

namespace ModelCMS.Account
{
    public class IplAccount : BaseIpl<ADOProvider>, IAccount
    {
        #region Methods

        /// <summary>
        /// Saves a record to the Account table.
        /// </summary>
        public long Insert(AccountEntity obj)
        {
            long res;            
            try
            {
                var p = Param(obj);
                var flag = unitOfWork.ProcedureExecute("Sp_Account_Insert", p);
                res = flag ? p.Get<long>("@Id") : 0;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
            return res;
        }

        /// <summary>
        /// Updates a record in the Account table.
        /// </summary>
        public bool Update(AccountEntity obj)
        {
            try
            {
                var p = Param(obj, "edit");
                var res = unitOfWork.ProcedureExecute("Sp_Account_Update", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the Account table by its primary key.
        /// </summary>
        public bool Delete(long iD, long adminId, int adminType)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@AccID", iD);
                p.Add("@AdminId", adminId);
                p.Add("@AdminType", adminType);
                var res = unitOfWork.ProcedureExecute("Sp_Account_Delete", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }
        public bool UpdateAvatar(AccountEntity obj)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", obj.Id);                
                var res = unitOfWork.ProcedureExecute("Sp_Account_UpdateAvatar", p);
                return res;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public AccountEntity GetAccountByEmail(string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@email", email);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_GetAccountByEmail", p).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }

        }

        /// <summary>
        /// Check UserName exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserNameExist(string userName)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                p.Add("@Res", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckUserNameExist", p);
                var data = p.Get<bool>("@Res");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool CheckEmailExist(string email, long id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);
                p.Add("@Id", id);
                p.Add("@Res", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckEmailExist", p);
                var data = p.Get<bool>("@Res");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool CheckResetPassword(string oldPass, string email)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@oldPass", oldPass);
                p.Add("@email", email);
                p.Add("@data", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_CheckResetPassword", p);
                var data = p.Get<bool>("@data");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }
        public bool ResetPassword(string email, string password)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@email", email);
                p.Add("@password", password);
                p.Add("@data", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                unitOfWork.ProcedureExecute("Sp_Account_ResetPassword", p);
                var data = p.Get<bool>("@data");
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// Selects a single record from the Account table.
        /// </summary>
        public AccountEntity ViewDetail(string id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ViewDetail", p).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                return null;
            }
        }

        public bool Login(string userName, string password, ref AccountEntity obj)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                p.Add("@password", password);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_Login", p).SingleOrDefault();                
                if (data != null)
                {
                    obj = data;
                    return true;
                }
                else
                {
                    obj = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        public bool ChangePassword(long id, string oldPassword, string newPassword)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                p.Add("@OldPassword", oldPassword);
                p.Add("@Password", newPassword);
                var data = unitOfWork.Procedure<string>("Sp_Account_ChangePassword", p).SingleOrDefault();
                return data == "SUCCESS";

            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the Account table.
        /// </summary>
        public List<AccountEntity> ListAll()
        {
            try
            {
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ListAll");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the Account table.
        /// </summary>
        public List<AccountEntity> ListAllPaging(AccountEntity AccountInfo, int pageIndex, int pageSize, string sortColumn, string sortDesc, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@UserName", AccountInfo.UserName);                
                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@sortColumn", sortColumn);
                p.Add("@sortDesc", sortDesc);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var data = unitOfWork.Procedure<AccountEntity>("Sp_Account_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");
                return data.ToList();
            }
            catch (Exception ex)
            {
                Logging.PutError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Saves a record to the Account table.
        /// </summary>
        private DynamicParameters Param(AccountEntity obj, string action = "add")
        {
            var p = new DynamicParameters();
            if (action == "add")
            {
                p.Add("@Type", obj.Type);
                p.Add("@UserName", obj.UserName);
                p.Add("@Password", obj.Password);
                p.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            }
            else if(action == "edit")
            {
                p.Add("@Id", obj.Id);
            }                       

            return p;
        }


        #endregion
    }
}
