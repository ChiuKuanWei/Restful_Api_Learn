using API_TEST.DTO_Class;
using API_TEST.GetData_Parameter;
using API_TEST.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//StatusCode:
//2xx 代表的是成功。像昨天我們測試 API 成功時，都是回傳 200 OK 這個狀態碼。
//3xx 代表的是轉向。通常是在原 API 的 URI 已換位址，所以用 3xx 開頭告知，通常也會帶要轉去哪裡的 URI 內容。
//4xx 代表的是客戶端的錯誤。像是在瀏覽網頁的時候，不小心打錯網址讓伺服器找不到網頁時，就會收到 404 Not Found 這個狀態碼。
//5xx 代表的是伺服器端的錯誤。例如：當伺服器在處理 HTTP request 出錯時，就會回傳 500 Internal Server Error。

namespace API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        //readonly 在建構子指派值之後，就不能更改
        private readonly CreativeTEMP_DBContext _CreativeTEMP_DBContext;
        private readonly IMapper _IMapper;

        public HelloWorldController(CreativeTEMP_DBContext CreativeTEMP_DBContext, IMapper IMapper)  //建構子區
        {
            _CreativeTEMP_DBContext = CreativeTEMP_DBContext;
            _IMapper = IMapper;
        }


        //GET: api/<HelloWorldController>
        //取得資源
        //取得資料表總攬
        //ActionResult為指定回傳型別
        //[HttpGet]
        //public ActionResult<IEnumerable<RgFreezer>> GetData()
        //{
        //    return _CreativeTEMP_DBContext.RgFreezers;  //回傳總攬
        //}

        // GET: api/<HelloWorldController>
        //取得指定資源
        //[HttpGet]
        //public IEnumerable<FreezerSelectDTO> Get_Select_Data()
        //{
        //    //DTO 修改成給使用者觀看的資訊-------------------------------------------------------
        //    var result = (from a in _CreativeTEMP_DBContext.RgFreezers
        //                  join b in _CreativeTEMP_DBContext.Groups on a.FreezerId equals b.Id
        //                  select new FreezerSelectDTO
        //                  {
        //                      FreezerId = a.FreezerId,
        //                      FloorId = a.FloorId,
        //                      FreezerName = a.FreezerName,
        //                      FreezerDesc = a.FreezerDesc,
        //                      CreativeDatetime = a.CreativeDatetime,
        //                      FREEZER_TOKEN = b.FreezerToken

        //                  });

        //    //var result1 = from a in _CreativeTEMP_DBContext.RgFreezers
        //    //              //where a.FloorId == 9
        //    //              select a;

        //    //var result2 = _CreativeTEMP_DBContext.RgFreezers.Where(e => e.FloorId == 9);

        //    return result;
        //}

        // GET: api/<HelloWorldController>?FreezerName=檢體儲存室&DateTime=2018-01-19
        //取得指定資源
        //FromQuery =>是從網址上的變數取得而來
        [HttpGet]
        public IEnumerable<FreezerSelectDTO> Get_Select_Data([FromQuery] Get_Select_Data_Parameter value)
        {
            //DTO 修改成給使用者觀看的資訊-------------------------------------------------------
            var result = (from a in _CreativeTEMP_DBContext.RgFreezers
                          join b in _CreativeTEMP_DBContext.Groups on a.FreezerId equals b.Id
                          select new FreezerSelectDTO
                          {
                              FreezerId = a.FreezerId,
                              FloorId = a.FloorId,
                              FreezerName = a.FreezerName,
                              FreezerDesc = a.FreezerDesc,
                              CreativeDatetime = a.CreativeDatetime,
                              FREEZER_TOKEN = b.FreezerToken,
                              //rgManagerDataDTOs = (from c in _CreativeTEMP_DBContext.RgManagerData
                              //                     where a.FreezerId == c.Id
                              //                     select new RgManagerDataDTO
                              //                     {
                              //                         Id = c.Id,
                              //                         LaboratoryUse = c.LaboratoryUse,
                              //                         ManagerName = c.ManagerName,
                              //                         ManagerNum = c.ManagerNum
                              //                     }).ToList()

                          });

            if (!string.IsNullOrEmpty(value.FreezerName))
            {
                result = result.Where(a => a.FreezerName == value.FreezerName);
            }
            if (value.DateTime != null)
            {
                result = result.Where(a => a.CreativeDatetime.Date == value.DateTime);
            }
            if(value.MinNumber != null && value.MaxNumber != null)
            {
                result = result.Where(a => a.FloorId >= value.MinNumber && a.FloorId <= value.MaxNumber);
            }

            return result;
        }

        //GET: api/<HelloWorldController>
        //利用AutoMapper改寫應給使用者觀看的欄位資料
        //[HttpGet]
        //public IEnumerable<FreezerSelectDTO> Get()
        //{
        //    //AutoMapper--------------------------------------------------------------------------------------------
        //    var result = _CreativeTEMP_DBContext.RgFreezers;
        //    return _IMapper.Map<IEnumerable<FreezerSelectDTO>>(result);
        //}

        // GET api/<HelloWorldController>/1 (5會傳入decimal ID)
        //取得資源
        //抓取對應的編號資料
        //ActionResult為指定回傳型別
        //FromRoute => 是從{ID}取得而來
        [HttpGet("{ID}")]
        public ActionResult<IEnumerable<FreezerSelectDTO>> /*FreezerSelectDTO*/ GetID([FromRoute] decimal ID)
        {
            //DTO 修改成給使用者觀看的資訊-------------------------------------------------------
            var result = (from a in _CreativeTEMP_DBContext.RgFreezers
                          join b in _CreativeTEMP_DBContext.Groups on a.FreezerId equals b.Id
                          where a.FreezerId == ID
                          select new FreezerSelectDTO
                          {
                              FreezerId = a.FreezerId,
                              FloorId = a.FloorId,
                              FreezerName = a.FreezerName,
                              FreezerDesc = a.FreezerDesc,
                              CreativeDatetime = a.CreativeDatetime,
                              FREEZER_TOKEN = b.FreezerToken,

                          }).SingleOrDefault(); //SingleOrDefault=>回傳一個結果或初始值NULL

            if (result == null)
            {
                return BadRequest("不符合對應資料條件!");
            }
            return Ok(result);

            //try
            //{
            //    var result = _CreativeTEMP_DBContext.RgFreezers.Find(ID);
            //    if (result == null)
            //    {
            //        return NotFound("找不到此ID編號!!");
            //    }
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        //GET: api/<HelloWorldController>/From/TEST1?id2=TEST2
        [HttpGet("From/{id1}")]
        public ArrayList GetFrom([FromRoute] string id1, [FromQuery] string id2, [FromForm] string id3)
        {         
           ArrayList AL = new ArrayList();
            AL.Add(id1);
            AL.Add(id2);
            AL.Add(id3);
            return AL;
        }

        // POST api/<HelloWorldController>
        //新增資源
        //ActionResult為指定回傳型別
        [HttpPost]
        public ActionResult<RgFreezer> Post([FromBody] RgFreezer value)
        {
            _CreativeTEMP_DBContext.RgFreezers.Add(value);
            _CreativeTEMP_DBContext.SaveChanges();

            return CreatedAtAction(nameof(GetID), new { ID = value.FreezerId }, value);
        }

        // PUT api/<HelloWorldController>/5 (5會傳入decimal id)
        //修改資源
        //IActionResult為不指定回傳型別
        [HttpPut("{id}")]
        public IActionResult Put(decimal id, [FromBody] RgFreezer value)
        {
            if (id != value.FreezerId)
            {
                return BadRequest("請輸入對應編號!");
            }

            try
            {
                _CreativeTEMP_DBContext.Entry(value).State = EntityState.Modified;
                _CreativeTEMP_DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (!_CreativeTEMP_DBContext.RgFreezers.Any(e => e.FreezerId == id))  //如果找不到任何一筆；序列的任何項目是否存在或符合條件。
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return Ok("修改成功!");  //回傳修改成功
            //return NoContent();  //不回傳任何內容
        }

        // DELETE api/<HelloWorldController>/5 (5會傳入decimal id)
        //刪除資源
        //IActionResult為不指定回傳型別
        [HttpDelete("{id}")]
        public IActionResult Delete(decimal id)
        {
            var delete = _CreativeTEMP_DBContext.RgFreezers.Find(id);
            if (delete == null)
            {
                return NotFound("找不到此編號!");
            }

            try
            {
                _CreativeTEMP_DBContext.RgFreezers.Remove(delete);
                _CreativeTEMP_DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500 ,ex.Message);
            }

            return Ok("刪除成功!");  //回傳刪除成功
            //return NoContent();  //不回傳任何內容
        }
    }
}

