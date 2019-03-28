using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 书章节,不包括书的内容
/// </summary>
public class BookDoc
{
    public Guid 上一章 { get; set; }

    public Guid 下一章 { get; set; }

    public string 章节名 { get; set; }

    public Guid 本记录GUID { get; set; }

    public decimal ID { get; set; }

}