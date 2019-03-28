using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 最新更新的内容 每一个书取的条的数据
/// </summary>
public class NewDocItem
{
    public decimal ID { get; set; }
    public Guid 本记录GUID
    { get; set; }
    public Guid GUID { get; set; }
    public string 书名 { get; set; }
    public long row { get; set; }
}