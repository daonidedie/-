/******************************************************
* 文件名：UpFile.cs
* 文件功能描述：	文件上传及类  命名空间 UpFile
* 
* 创建标识：周渊 2004-11-33
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.IO;

namespace Skybot.Tong.UpFile
{
    #region	文件上传及类 只能上传单个文件 命名空间 Bocom.Project.Library
    /*
     Descript:上传文件
     Author:Blue.Dream
     Date:2004-09-21 22:21  
    */

    /// <summary>
    /// 上传单个文件 页面一定要有一个文件域 Web控件
    /// 示例
    ///UpLoadFile upx = new UpLoadFile("jpg,gif,bmp,Psd",200000,Server.MapPath("./"));
    ///upx.UpLoad();
    ///strState.Text = upx.FileName;
    /// </summary>
    public class UpLoadFile
    {
        #region 字段
        /// <summary>
        /// //所允许的文件类型
        /// </summary>
        private string[] _allowFileType;

        /// <summary>
        /// //所允许的文件大小(KB)
        /// </summary>
        private double _fileLength;

        /// <summary>
        /// //文件的存储路径
        /// </summary>
        private string _savePath;

        /// <summary>
        /// //上传后的文件名
        /// </summary>
        private string _saveFile;

        /// <summary>
        /// //存储错误信息
        /// </summary>
        private string _error;

        /// <summary>
        /// 上传文件的扩展名
        /// </summary>
        private string _fileExtesion;

        #endregion


        #region 公共方法

        /// <summary>
        /// 上传文件主构造函数
        /// </summary>
        /// <param name="allFileType">允许的文件类型，多个以","分开</param>
        /// <param name="fileLength">文件大小</param>
        /// <param name="savePath">保存路径</param>
        public UpLoadFile(string allFileType, double fileLength, string savePath)
        {
            char[] sp = { ',' };
            _allowFileType = allFileType.Split(sp);
            _fileLength = fileLength;
            _savePath = savePath;
        }
        /// <summary>
        /// 文件上传调上传
        /// </summary>
        /// <returns></returns>
        public bool UpLoad()
        {
            bool result = true;
            System.Web.HttpFileCollection objFiles = System.Web.HttpContext.Current.Request.Files;
            System.Web.HttpPostedFile objFile = objFiles[0];
            try
            {
                //查看文件长度
                if (objFile.ContentLength > (this._fileLength))
                {
                    this._error = "文件大小超出范允许的范围！";
                    return false;
                }

                string fileName = Path.GetFileName(objFile.FileName);
                this._fileExtesion = Path.GetExtension(fileName);

                if (!CheckFileExt(this._fileExtesion.ToLower()))
                {
                    this._error = "文件类型" + this._fileExtesion + "不允许！";
                    return false;
                }
                //取得要保存的文件名
                string UpFile = this.MakeFileName(this._fileExtesion);
                //保存文件
                objFile.SaveAs(UpFile);
                //返回文件名
                this._saveFile = Path.GetFileName(UpFile);

            }
            catch (Exception e)
            {
                result = false;
                this._error = e.Message;
            }
            return result;
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 返回生成的文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return _saveFile;
            }
        }

        /// <summary>
        /// 返回出错信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _error;
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 根据SavePath,生成文件名
        /// </summary>
        /// <returns></returns>
        private string MakeFileName(string fileType)
        {
            string file = this._savePath + "\\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + this._fileExtesion;
            for (; File.Exists(file); )
            {
                file = this._savePath + "\\" + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + this._fileExtesion;
            }
            return file;
        }

        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="fileEx">MIME内容</param>
        /// <returns></returns>
        private bool CheckFileExt(string fileEx)
        {
            bool result = false;
            for (int i = 0; i < this._allowFileType.Length; i++)
            {
                if (fileEx.IndexOf(this._allowFileType[i].ToLower()) > -1)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        #endregion
    }
    #endregion
}