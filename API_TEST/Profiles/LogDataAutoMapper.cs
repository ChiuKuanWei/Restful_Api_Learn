using API_TEST.DTO_Class;
using API_TEST.Models;
using AutoMapper;

namespace API_TEST.Profiles
{
    public class LogDataAutoMapper:Profile
    {
        public LogDataAutoMapper()
        {
            //CreateMap<RgFreezer, FreezerSelectDTO>();  //LogData與LogDataSelectDTO參數名稱一樣時
            CreateMap<RgFreezer, FreezerSelectDTO>().ForMember(a => a.FREEZER_TOKEN, b => b.MapFrom(c => c.FloorId + " " + c.FreezerName));  //LogData與LogDataSelectDTO參數名稱不一樣時
        }
    }
}
