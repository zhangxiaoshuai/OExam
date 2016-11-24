﻿using Component.Tools;
using OExam.App.Business;
using OExam.App.ViewModels;
using OExam.Server.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OExam.Server.Controllers
{
    
    public class UserManagerController : ApiController
    {
        UserMessage _userMessage;
        public UserManagerController()
        {
            _userMessage = new UserMessage();
        }

        // GET: api/UserManager
        [Anonymous]
        public OperationResult<UserData> Get(string username,string password,int roletype)
        {
            LoginUser luser = new LoginUser();
            luser.LoginName = username;
            luser.LoginPassword = password;
            luser.LoginType = roletype;
            OperationResult<UserData> oresult = _userMessage.Login(luser);
            //OperationResult oresult = new OperationResult(EOperationType.Success, "successmessage"+username);
            //登录成功存储令牌，并返回给客户端
            if(oresult.ResultType == EOperationType.Success)
            {
                oresult.Message = username + TokenCache.TOKENSPLIT + TokenCache.SaveToken(username);
            }

            return oresult; 
        }
        ///// <summary>
        ///// 访问路径 http://localhost:29279/aaa/bb
        ///// </summary>
        ///// <returns></returns>
        //[Route("aaa/bb")]
        //public string getuser()
        //{
        //    return "success";
        //}
        // GET: api/UserManager/5
        public OperationResult Get(int id)
        {
            OperationResult oresult = new OperationResult(EOperationType.Success, "successmessage"+id);
            return oresult;
        }

        // POST: api/UserManager
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserManager/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserManager/5
        public void Delete(int id)
        {
        }
    }
}
