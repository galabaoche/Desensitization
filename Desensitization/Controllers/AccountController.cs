using Desensitization.Data;
using Desensitization.Desensitize;
using Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Controllers
{

    public class AccountController : ControllerBase
    {
        private readonly IMongoCollection<CustomStrategy> _customStrategy;
        private readonly IMongoCollection<PropertyRule> _propertyRule;
        public AccountController()
        {
            var client = new MongoClient("mongodb://140.143.130.42:7017");
            var database = client.GetDatabase("Desensitization");
            _customStrategy= database.GetCollection<CustomStrategy>("CustomStrategy");
            _propertyRule = database.GetCollection<PropertyRule>("PropertyRule");
        }
        //[DesensitizeFilter]
        public ActionResult Index()
        {
            IList<AccountDto> accountList = new List<AccountDto>()
            {
                 new AccountDto{
                    OutIDNumber="6225561450094476",
                    AccountNumber="1004508242000002",
                    CustomerId=234,
                    CustomerName="来自月球的客户",
                    ProvinceName="河北",
                    CityName="秦皇岛",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="凌达"
                    }
                },
                new AccountDto{
                    OutIDNumber="6225561450094476",
                    AccountNumber="1004508242000002",
                    CustomerId=67,
                    CustomerName="浦发",
                    ProvinceName="河北",
                    CityName="秦皇岛",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="凌达"
                    }
                },
                new AccountDto{
                    OutIDNumber="6275561450094476",
                    AccountNumber="1094508243000002",
                    CustomerId=67,
                    CustomerName="浦发",
                    ProvinceName="河北",
                    CityName="石家庄",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="唐二毛"
                    }
                },
                new AccountDto{
                    OutIDNumber="6225561450095678",
                    AccountNumber="1004508242000002",
                    CustomerId=67,
                    CustomerName="浦发",
                    ProvinceName="河北",
                    CityName="唐山",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="张三丰"
                    }
                },
                new AccountDto{
                    OutIDNumber="6225561450094476",
                    AccountNumber="1004508242000002",
                    CustomerId=65,
                    CustomerName="未知",
                    ProvinceName="河南",
                    CityName="洛阳",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="李四"
                    }
                },
                new AccountDto{
                    OutIDNumber="6225561450094476",
                    AccountNumber="10045082420000021",
                    CustomerId=69,
                    CustomerName="未知",
                    ProvinceName="河北",
                    CityName="邯郸",
                    Debtor=new DebtorDto
                    {
                        IDNumber="232321196911082917",
                        DebtorName="王五"
                    }
                },
            };

            // accountList.Desensitizate();
            //return Json(accountList,JsonRequestBehavior.AllowGet);
            var customStrategyFilter = Builders<CustomStrategy>.Filter.Empty;
            var propertyRuleFilter = Builders<PropertyRule>.Filter.Empty;

            var customStrategyList = _customStrategy.Find(customStrategyFilter).ToList();
            var propertyRuleList = _propertyRule.Find(propertyRuleFilter).ToList();
            return Desensitizate(accountList);
        }
    }
}