using Desensitization.Desensitize;
using Desensitization.Desensitize.Extensions;
using Desensitization.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Desensitization.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            IList<AccountDto> accountList = new List<AccountDto>()
            {
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
            accountList.Desensitizate();
            return Json(accountList, JsonRequestBehavior.AllowGet);
        }
    }
}