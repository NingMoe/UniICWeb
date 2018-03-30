using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_CheckLink : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        CAMPUSREQ req=new CAMPUSREQ();
        UNICAMPUS[] rlt;
        if (m_Request.Account.CampusGet(req,out rlt)==REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("+ok");
        }
        else
        {
            Response.Write("timeout");
        }
    }
//    public delegate void DoHandler();
//    public class Timeout
//    {
//        private ManualResetEvent mTimeoutObject;
//        //标记变量
//private bool mBoTimeout;
//        public DoHandler Do;

//        public Timeout()
//        {
//            //  初始状态为 停止
//this.mTimeoutObject =new ManualResetEvent(true);
//        }
//        ///<summary>
//        /// 指定超时时间 异步执行某个方法
//        ///</summary>
//        ///<returns>执行 是否超时</returns>
//public bool DoWithTimeout(TimeSpan timeSpan)
//        {
//            if (this.Do ==null)
//            {
//                return false;
//            }
//            this.mTimeoutObject.Reset();
//            this.mBoTimeout =true; //标记
//this.Do.BeginInvoke(DoAsyncCallBack, null);
//            // 等待 信号Set
//if (!this.mTimeoutObject.WaitOne(timeSpan, false))
//            {
//                this.mBoTimeout =true;
//            }
//            return this.mBoTimeout;
//        }
//        ///<summary>
//        /// 异步委托 回调函数
//        ///</summary>
//        ///<param name="result"></param>
//private void DoAsyncCallBack(IAsyncResult result)
//        {
//            try
//            {
//                this.Do.EndInvoke(result);
//                // 指示方法的执行未超时
//this.mBoTimeout =false; 
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                this.mBoTimeout =true;
//            }
//            finally
//            {
//                this.mTimeoutObject.Set();
//            }
//        }
//    }
}
//class Program
//{
//    private static Stopwatch watch;
//    private static System.Threading.Timer timer;

//    [STAThread]
//    static void Main(string[] args)
//    {
//        watch = new Stopwatch();
//        Timeout timeout = new Timeout();
//        timeout.Do = new Program().DoSomething;
//        watch.Start();
//        timer = new System.Threading.Timer(timerCallBack, null, 0, 500);
//        Console.WriteLine("4秒超时开始执行");
//        bool bo = timeout.DoWithTimeout(new TimeSpan(0, 0, 0, 4));
//        Console.WriteLine(string.Format("4秒超时执行结果,是否超时：{0}", bo));
//        Console.WriteLine("***************************************************");

//        timeout = new Timeout();
//        timeout.Do = new Program().DoSomething;
//        Console.WriteLine("6秒超时开始执行");
//        bo = timeout.DoWithTimeout(new TimeSpan(0, 0, 0, 6));
//        Console.WriteLine(string.Format("6秒超时执行结果,是否超时：{0}", bo));

//        timerCallBack(null);

//        watch.Stop();
//        timer.Dispose();
//        Console.ReadLine();
//    }
//    static void timerCallBack(object obj)
//    {
//        Console.WriteLine(string.Format("运行时间:{0}秒", watch.Elapsed.TotalSeconds.ToString("F2")));
//    }
//    public void DoSomething()
//    {
//        // 休眠 5秒
//        System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 5));
//    }
//}