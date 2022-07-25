using System.Text.RegularExpressions;

namespace API_TEST.GetData_Parameter
{
    public class Get_Select_Data_Parameter
    {
        public string? FreezerName { get; set; }
        public DateTime? DateTime { get; set; }
        public int? MinNumber { get; set; }
        public int? MaxNumber { get; set; }

        //方便化方法
        private string? _Number;
        public string? Number
        {
            get { return _Number; }
            set
            {
                //2-4
                Regex regex = new Regex(@"^\d*-\d*$");
                if (regex.Match(value).Success)  //在輸入字串搜尋規則運算式的項目，並傳回正確結果為單一規則運算式 (Regular Expression) 比對的結果。
                {
                    MinNumber = Int32.Parse(value.Split('-')[0]);
                    MaxNumber = Int32.Parse(value.Split('-')[1]);
                }

                _Number = value;
            }
        }
    }
}


//常用語法說明

//特殊字元
//\   : 後面為特殊符號 ,e.g  \\ 代表\  ,  \n 代表 換行, \( 代表(
//^  : 字串開始
//$ : 字串結尾
//. : 任何字元 除了\n

//開頭 與結尾

//(?=Pattern) : 前面為pattern       , e.g Patternabc
//(?<=Pattern): 後面為pattern    , e.g abcPattern

//集合及字元
//[abc] : 字元集合, e.g[a - zA - Z]  :英文大小寫
//[^a - z]: 非a - z , e.g ABCdefGH  ==>  [^a-z]  ==> ABC 與 GH
//\d : 數字 ,e.g ABC123DEF==>[\d]*  ==> 123
//\D: 非數字,e.g ABC123DEF ==>[\D]* ==> ABC 與 DEF
//\s : 一個空白字元
//\S: 非空白字元
//\w: 單詞字元(a - z, A - Z, 0 - 9, _)  ,e.g New York -->\w+==>  New 與 York
//\W:非單詞字元


//出現次數
//* :0~n次
//+ :1~n次
//?: 0-1次
//{n}:n次
//{n,m} :n~m次

//優先順序:
//1.  \
//2.  ()(?=)..[]
//3.  * + ?   { n,m}
//4. ^ $
//5.  | 