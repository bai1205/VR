using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FileUtils
{
    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public static void FileCreate(string name, byte[] data, string path)
    {
        FileStream fileStream = new FileStream(path + name, FileMode.Create);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    public static void RemoveFile(string path)
    {
        File.Delete(path);
    }

    /// 将 Stream 转成 byte[]

    public static byte[] StreamToBytes(Stream stream)
    {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        // 设置当前流的位置为流的开始 
        stream.Seek(0, SeekOrigin.Begin);
        return bytes;
    }

    /// <summary>  
    /// 根据图片路径返回图片的字节流byte[]  
    /// </summary>  
    /// <param name="imagePath">图片路径</param>  
    /// <returns>返回的字节流</returns>  
    public static byte[] GetImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }

    /// <summary>
    /// IO读取
    /// </summary>
    /// <returns></returns>
    public static IEnumerator ReadData(string path,Action<string> action)
    {
        string readData;
        string fileUrl = path;
        //读取文件
        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //数据保存
            readData = sr.ReadToEnd();
            sr.Close();
        }
        //返回数据
        if (action!=null )
        {
            action(readData);
        }
        Debug.Log("打印读取的技能Json：" + readData);
        yield return null;
    }

}

