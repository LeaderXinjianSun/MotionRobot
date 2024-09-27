using gts;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace MotionRobot.Models
{
    public struct AxisStatus
    {
        public bool FlagHome;
        public bool FlagAlm;
        public bool FlagMError;
        public bool FlagPosLimit;
        public bool FlagNeglimit;
        public bool FlagPosSoftLim;
        public bool FlagNegSoftLim;
        public bool FlagEmgStop;
        public bool FlagSmoothStop;
        public bool FlagAbruptStop;
        public bool FlagServoOn;
        public bool FlagMoveEnd;
        public double PrfPos;
        public double EncPos;
        public ushort MoveMode;
    }
    public struct AxisParm
    {
        public short CardNo;
        public short AxisId;
        public double Equiv;//换算成mm或°的齿轮比
        public short HomeDir;
        public short HomeMode;
        public double HomeHSpd;
        public double HomeLSpd;
        public double HomeOffset;
        public double Acc;
        public double MaxWorkSpd;
        public double PosLimit;
        public double NegLimit;
        public bool HomeFinish;
        public AxisStatus AxisSts;
        public double SafePos;
        public double Target;
    }
    public struct PTData
    {
        public double pos;
        public int time;
    }
    public class M1Point
    {
        public int IOIndex { get; set; }
        public bool OnOff { get; set; }
        public double Delay { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double MidX { get; set; }
        public double MidY { get; set; }
        public double MidZ { get; set; }
        public double Radius { get; set; }
        public int CircleDir { get; set; }
        public double Speed { get; set; }
        public double Acc { get; set; }
        public ushort Type { get; set; }
    }
    public static class GTSCard
    {
        public static AxisStatus GetAxisStatus(AxisParm axisParm)
        {
            int AxisStatus = 0;
            uint temp_pClock = 0;
            gts.mc.GT_GetSts(axisParm.CardNo, axisParm.AxisId, out AxisStatus, 1, out temp_pClock);
            int pValue;
            AxisStatus result = default(AxisStatus);
            result.FlagServoOn = (AxisStatus & 0x200) == 0x200;
            result.FlagAlm = (AxisStatus & 0x2) == 0x2;
            result.FlagPosLimit = (AxisStatus & 0x20) == 0x20;
            result.FlagNeglimit = (AxisStatus & 0x40) == 0x40;
            result.FlagMoveEnd = (AxisStatus & 0x400) != 0x400;
            result.FlagEmgStop = (AxisStatus & 0x100) == 0x100;
            result.FlagSmoothStop = (AxisStatus & 0x80) == 0x80;

            gts.mc.GT_GetDi(axisParm.CardNo, gts.mc.MC_HOME, out pValue);
            result.FlagHome = (pValue & (1 << axisParm.AxisId - 1)) == 0;
            double pValue1;
            uint pClock;
            gts.mc.GT_GetPrfPos(axisParm.CardNo, axisParm.AxisId, out pValue1, 1, out pClock);
            result.PrfPos = pValue1 * axisParm.Equiv;
            gts.mc.GT_GetEncPos(axisParm.CardNo, axisParm.AxisId, out pValue1, 1, out pClock);
            result.EncPos = pValue1 * axisParm.Equiv;
            return result;
        }
        public static bool GetAxisAlarm(AxisParm axisParm)
        {
            int AxisStatus = 0;
            uint temp_pClock = 0;
            gts.mc.GT_GetSts(axisParm.CardNo, axisParm.AxisId, out AxisStatus, 1, out temp_pClock);
            return (AxisStatus & 0x2) == 0x2;
        }
        public static double GetPos(AxisParm axisParm)
        {
            double pValue1;
            uint pClock;
            gts.mc.GT_GetPrfPos(axisParm.CardNo, axisParm.AxisId, out pValue1, 1, out pClock);
            return pValue1 * axisParm.Equiv;
        }

        public static double GetEnc(AxisParm axisParm)
        {
            double pValue1;
            uint pClock;
            gts.mc.GT_GetEncPos(axisParm.CardNo, axisParm.AxisId, out pValue1, 1, out pClock);
            return pValue1 * axisParm.Equiv;
        }

        public static void ServoOn(AxisParm axisParm)
        {
            gts.mc.GT_AxisOn(axisParm.CardNo, axisParm.AxisId);
        }

        public static void ServoOff(AxisParm axisParm)
        {
            gts.mc.GT_AxisOff(axisParm.CardNo, axisParm.AxisId);
        }

        public static void ClearAlm(AxisParm axisParm)
        {
            gts.mc.GT_SetDoBit(axisParm.CardNo, gts.mc.MC_CLEAR, axisParm.AxisId, 0);
            System.Threading.Thread.Sleep(1000);
            gts.mc.GT_SetDoBit(axisParm.CardNo, gts.mc.MC_CLEAR, axisParm.AxisId, 1);
            gts.mc.GT_ClrSts(axisParm.CardNo, axisParm.AxisId, 1);
        }
        public static void ClearAlm1(AxisParm axisParm)
        {
            gts.mc.GT_ClrSts(axisParm.CardNo, axisParm.AxisId, 1);
        }
        public static bool GetDi(short cardNum, ushort bitno)
        {
            int pValue;
            gts.mc.GT_GetDi(cardNum, gts.mc.MC_GPI, out pValue);
            return (pValue & (1 << bitno)) == 0;
        }

        public static void SetDo(short cardNum, short outbit, short value)
        {
            gts.mc.GT_SetDoBit(cardNum, gts.mc.MC_GPO, (short)(outbit + 1), value);
        }

        public static int GetDiPort1(short cardNum)
        {
            int pValue;
            gts.mc.GT_GetDi(cardNum, gts.mc.MC_GPI, out pValue);
            return pValue;
        }

        public static int GetDoPort1(short cardNum)
        {
            int pValue;
            gts.mc.GT_GetDo(cardNum, gts.mc.MC_GPO, out pValue);
            return pValue;
        }
        public static void SetDoPort1(short cardNum, int value)
        {
            gts.mc.GT_SetDo(cardNum, gts.mc.MC_GPO, value);
        }

        public static void AxisHomeMove(AxisParm _axisParam)
        {
            gts.mc.GT_ZeroPos(_axisParam.CardNo, _axisParam.AxisId, 1);
            gts.mc.THomePrm tHomePrm;
            short SRtn = gts.mc.GT_GetHomePrm(_axisParam.CardNo, _axisParam.AxisId, out tHomePrm);
            tHomePrm.mode = _axisParam.HomeMode;//gts.mc.HOME_MODE_LIMIT_HOME
            tHomePrm.moveDir = _axisParam.HomeDir;
            tHomePrm.indexDir = (short)(_axisParam.HomeDir * -1);
            tHomePrm.edge = 1;
            tHomePrm.pad2_1 = 1;
            tHomePrm.velHigh = _axisParam.HomeHSpd / _axisParam.Equiv / 1000;
            tHomePrm.velLow = _axisParam.HomeLSpd / _axisParam.Equiv / 1000;
            tHomePrm.acc = _axisParam.Acc / 10;
            tHomePrm.dec = _axisParam.Acc / 10;
            tHomePrm.smoothTime = 25;
            tHomePrm.searchHomeDistance = 500000;
            tHomePrm.searchIndexDistance = 80000;
            tHomePrm.escapeStep = 5000;
            gts.mc.GT_GoHome(_axisParam.CardNo, _axisParam.AxisId, ref tHomePrm);//启动 Smart Home 回原点
        }

        public static bool AxisHomeCheckDone(AxisParm _axisParam)
        {
            gts.mc.THomeStatus tHomeSts;
            gts.mc.GT_GetHomeStatus(_axisParam.CardNo, _axisParam.AxisId, out tHomeSts);
            if (tHomeSts.run == 0)
            {
                if (tHomeSts.stage == gts.mc.HOME_STAGE_END)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void AxisZeroSet(AxisParm _axisParam)
        {
            gts.mc.GT_ZeroPos(_axisParam.CardNo, _axisParam.AxisId, 1);//清零规划位置和实际位置，并进行零漂补偿。
        }

        public static void AxisPosSet(AxisParm _axisParam, double value)
        {
            gts.mc.GT_SetPrfPos(_axisParam.CardNo, _axisParam.AxisId, (int)Math.Round(value / _axisParam.Equiv, 0));
        }

        public static void AxisEncSet(AxisParm _axisParam, double value)
        {
            gts.mc.GT_SetEncPos(_axisParam.CardNo, _axisParam.AxisId, (int)Math.Round(value / _axisParam.Equiv, 0));
        }

        public static void AxisPosMove(ref AxisParm _AxisParam, double givePos, double speed = 0, double acc = 0)
        {
            _AxisParam.Target = givePos / _AxisParam.Equiv;
            gts.mc.TTrapPrm ATrapPrm = new gts.mc.TTrapPrm();
            gts.mc.GT_PrfTrap(_AxisParam.CardNo, _AxisParam.AxisId);
            gts.mc.GT_GetTrapPrm(_AxisParam.CardNo, _AxisParam.AxisId, out ATrapPrm); //读取点位模式运动参数
            if (acc == 0)
            {
                ATrapPrm.acc = _AxisParam.Acc;
                ATrapPrm.dec = _AxisParam.Acc;
            }
            else
            {
                ATrapPrm.acc = acc;
                ATrapPrm.dec = acc;
            }
            ATrapPrm.smoothTime = (short)25;
            gts.mc.GT_SetTrapPrm(_AxisParam.CardNo, _AxisParam.AxisId, ref ATrapPrm); //设置点位模式运动参数
            gts.mc.GT_SetPos(_AxisParam.CardNo, _AxisParam.AxisId, (int)Math.Round(givePos / _AxisParam.Equiv, 0)); //设置目标位置
            double max_Vel;
            if (speed == 0.0)
            {
                max_Vel = _AxisParam.MaxWorkSpd / _AxisParam.Equiv / 1000;
            }
            else
            {
                max_Vel = speed / _AxisParam.Equiv / 1000;
            }
            gts.mc.GT_SetVel(_AxisParam.CardNo, _AxisParam.AxisId, max_Vel); //设置目标速度
            gts.mc.GT_Update(_AxisParam.CardNo, 1 << (_AxisParam.AxisId - 1)); //启动当前轴运动
        }

        public static bool AxisPosMoveCheckDone(AxisParm _AxisParam)
        {
            int AxisStatus = 0;
            uint temp_pClock = 0;
            gts.mc.GT_GetSts(_AxisParam.CardNo, _AxisParam.AxisId, out AxisStatus, 1, out temp_pClock);
            if ((AxisStatus & 0x400) != 0x400)
            {
                double pValue1;
                uint pClock;
                gts.mc.GT_GetEncPos(_AxisParam.CardNo, _AxisParam.AxisId, out pValue1, 1, out pClock);
                if (Math.Abs(pValue1 - _AxisParam.Target) < 100)
                {
                    return true;
                }
            }
            return false;
        }

        public static void AxisJog(AxisParm _AxisParam, ushort dir, double spd = 0)
        {
            double Vel_JSpeed = 0;
            if (dir == 0)//反向
            {
                Vel_JSpeed = spd / _AxisParam.Equiv / 1000 * -1;
            }
            else
            {
                Vel_JSpeed = spd / _AxisParam.Equiv / 1000;
            }
            gts.mc.TJogPrm JogPrm = new gts.mc.TJogPrm();
            gts.mc.GT_PrfJog(_AxisParam.CardNo, _AxisParam.AxisId);
            gts.mc.GT_GetJogPrm(_AxisParam.CardNo, _AxisParam.AxisId, out JogPrm);
            JogPrm.acc = 1;
            JogPrm.dec = 1;
            gts.mc.GT_SetJogPrm(_AxisParam.CardNo, _AxisParam.AxisId, ref JogPrm);
            gts.mc.GT_SetVel(_AxisParam.CardNo, _AxisParam.AxisId, Vel_JSpeed); //设置当前轴的目标速度
            gts.mc.GT_Update(_AxisParam.CardNo, (1 << (_AxisParam.AxisId - 1))); //启动当前轴运动
        }

        public static void AxisStop(AxisParm _AxisParam, int type)
        {
            gts.mc.GT_Stop(_AxisParam.CardNo, 1 << (_AxisParam.AxisId - 1), type << (_AxisParam.AxisId - 1));
        }

        //public static bool SetCrd(short cardNum, int ZIndex, short crd)
        //{
        //    gts.mc.TCrdPrm crdPrm;

        //    crdPrm.dimension = 3;                        // 建立三维的坐标系
        //    crdPrm.synVelMax = 2000;                      // 坐标系的最大合成速度是: 500 pulse/ms
        //    crdPrm.synAccMax = 20;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2
        //    crdPrm.evenTime = 0;                         // 坐标系的最小匀速时间为0
        //    switch (crd)
        //    {
        //        case 1:
        //            crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
        //            crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
        //            crdPrm.profile3 = 3;
        //            break;
        //        case 2:
        //        default:
        //            crdPrm.profile1 = 5;                       // 规划器1对应到X轴                       
        //            crdPrm.profile2 = 6;                       // 规划器2对应到Y轴
        //            crdPrm.profile3 = 7;
        //            break;
        //    }

        //    crdPrm.profile4 = 0;
        //    crdPrm.profile5 = 0;
        //    crdPrm.profile6 = 0;
        //    crdPrm.profile7 = 0;
        //    crdPrm.profile8 = 0;
        //    crdPrm.setOriginFlag = 0;                    // 需要设置加工坐标系原点位置
        //    crdPrm.originPos1 = 0;                     // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
        //    crdPrm.originPos2 = 0;
        //    crdPrm.originPos3 = 0;
        //    crdPrm.originPos4 = 0;
        //    crdPrm.originPos5 = 0;
        //    crdPrm.originPos6 = 0;
        //    crdPrm.originPos7 = 0;
        //    crdPrm.originPos8 = 0;

        //    short SRtn = gts.mc.GT_SetCrdPrm(cardNum, crd, ref crdPrm);
        //    if (SRtn != 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public static bool SetCrd3D(short cardNum, short crd,bool isAbs, short x, short y, short z, double maxSpeed, double maxAcc)
        {
            try
            {
                gts.mc.TCrdPrm crdPrm;

                crdPrm.dimension = 3;                        // 建立二维的坐标系
                crdPrm.synVelMax = maxSpeed;                      // 坐标系的最大合成速度是: 500 pulse/ms
                crdPrm.synAccMax = maxAcc;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2
                crdPrm.evenTime = 0;                         // 坐标系的最小匀速时间为0
                short[] axisArr = new short[8];
                //找X
                axisArr[x - 1] = 1;
                //找Y
                axisArr[y - 1] = 2;
                //找Z
                axisArr[z - 1] = 3;
                crdPrm.profile1 = axisArr[0];                       // 规划器1对应到X轴                       
                crdPrm.profile2 = axisArr[1];                       // 规划器2对应到Y轴
                crdPrm.profile3 = axisArr[2];
                crdPrm.profile4 = axisArr[3];
                crdPrm.profile5 = axisArr[4];
                crdPrm.profile6 = axisArr[5];
                crdPrm.profile7 = axisArr[6];
                crdPrm.profile8 = axisArr[7];
                crdPrm.setOriginFlag = (short)(isAbs ? 1 : 0);                    // 需要设置加工坐标系原点位置
                crdPrm.originPos1 = 0;                     // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
                crdPrm.originPos2 = 0;
                crdPrm.originPos3 = 0;
                crdPrm.originPos4 = 0;
                crdPrm.originPos5 = 0;
                crdPrm.originPos6 = 0;
                crdPrm.originPos7 = 0;
                crdPrm.originPos8 = 0;

                short SRtn = gts.mc.GT_SetCrdPrm(cardNum, crd, ref crdPrm);
                if (SRtn != 0)
                {
                    return false;
                }

                gts.mc.TCrdData[] crdData = new mc.TCrdData[200];

                SRtn = gts.mc.GT_InitLookAhead(cardNum, crd, 0, 10, maxAcc, 200, ref crdData[0]);
                if (SRtn != 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SetCrd2D(short cardNum, short crd, short x, short y, double maxSpeed, double maxAcc)
        {
            try
            {
                gts.mc.TCrdPrm crdPrm;

                crdPrm.dimension = 2;                        // 建立二维的坐标系
                crdPrm.synVelMax = maxSpeed;                      // 坐标系的最大合成速度是: 500 pulse/ms
                crdPrm.synAccMax = maxAcc;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2
                crdPrm.evenTime = 0;                         // 坐标系的最小匀速时间为0
                short[] axisArr = new short[8];
                //找X
                axisArr[x - 1] = 1;
                //找Y
                axisArr[y - 1] = 2;
                crdPrm.profile1 = axisArr[0];                       // 规划器1对应到X轴                       
                crdPrm.profile2 = axisArr[1];                       // 规划器2对应到Y轴
                crdPrm.profile3 = axisArr[2];
                crdPrm.profile4 = axisArr[3];
                crdPrm.profile5 = axisArr[4];
                crdPrm.profile6 = axisArr[5];
                crdPrm.profile7 = axisArr[6];
                crdPrm.profile8 = axisArr[7];
                crdPrm.setOriginFlag = 0;                    // 需要设置加工坐标系原点位置
                crdPrm.originPos1 = 0;                     // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
                crdPrm.originPos2 = 0;
                crdPrm.originPos3 = 0;
                crdPrm.originPos4 = 0;
                crdPrm.originPos5 = 0;
                crdPrm.originPos6 = 0;
                crdPrm.originPos7 = 0;
                crdPrm.originPos8 = 0;

                short SRtn = gts.mc.GT_SetCrdPrm(cardNum, crd, ref crdPrm);
                if (SRtn != 0)
                {
                    return false;
                }

                gts.mc.TCrdData[] crdData = new mc.TCrdData[200];

                SRtn = gts.mc.GT_InitLookAhead(cardNum, crd, 0, 5, maxAcc, 200, ref crdData[0]);
                if (SRtn != 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public static void AxisLnXYZMove(AxisParm XY, AxisParm Z, List<M1Point> targets, short crd, double acc)
        //{
        //    gts.mc.GT_CrdClear(XY.CardNo, crd, 0);
        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        switch (targets[i].Type)
        //        {
        //            case 0://普通定位
        //                gts.mc.GT_LnXYZ(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), (int)(targets[i].Z / Z.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0, 0);
        //                break;
        //            case 1://精确定位
        //                gts.mc.GT_LnXYZG0(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), (int)(targets[i].Z / Z.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0);
        //                break;
        //            case 2://延时
        //                gts.mc.GT_BufDelay(XY.CardNo, crd, (ushort)targets[i].X, 0);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    gts.mc.GT_CrdStart(XY.CardNo, 1, 0);
        //}
        //public static void AxisLnXYMove(AxisParm XY, List<M1Point> targets, short crd, double acc)
        //{
        //    gts.mc.GT_CrdClear(XY.CardNo, crd, 0);
        //    for (int i = 0; i < targets.Count; i++)
        //    {
        //        switch (targets[i].Type)
        //        {
        //            case 0://普通定位
        //                gts.mc.GT_LnXY(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0, 0);
        //                break;
        //            case 1://精确定位
        //                gts.mc.GT_LnXYG0(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0);
        //                break;
        //            case 2://延时
        //                gts.mc.GT_BufDelay(XY.CardNo, crd, (ushort)(targets[i].Delay * 1000), 0);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
        public static void AxisLnXYMove3D(AxisParm XAxis, AxisParm YAxis, AxisParm ZAxis, List<M1Point> targets, short crd, double acc)
        {
            // 指令返回值
            short sRtn;
            int space;
            sRtn = gts.mc.GT_CrdClear(XAxis.CardNo, crd, 0);
            for (int i = 0; i < targets.Count; i++)
            {
                double _acc = acc <= 0 ? targets[i].Acc : acc;
                switch (targets[i].Type)
                {
                    case 0://直线普通定位
                        sRtn = gts.mc.GT_LnXYZ(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv), (int)(targets[i].Z / ZAxis.Equiv), targets[i].Speed / XAxis.Equiv / 1000, _acc, 0, 0);
                        //// 查询返回值是否成功
                        //if (0 != sRtn)
                        //{
                        //    do
                        //    {
                        //        // 查询运动缓存区空间，直至空间不为0
                        //        sRtn = gts.mc.GT_CrdSpace(XY.CardNo, crd, out space, 0);
                        //    } while (0 == space);
                        //    // 重新调用上次失败的插补指令
                        //    sRtn = gts.mc.GT_LnXY(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0, 0);
                        //}
                        break;
                    case 1://直线精确定位
                        sRtn = gts.mc.GT_LnXYZG0(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv), (int)(targets[i].Z / ZAxis.Equiv), targets[i].Speed / XAxis.Equiv / 1000, _acc, 0);
                        break;
                    case 2://圆弧定位
                        sRtn = gts.mc.GT_ArcXYZ(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv), (int)(targets[i].Z / ZAxis.Equiv),
                            (int)(targets[i].MidX / XAxis.Equiv), (int)(targets[i].MidY / YAxis.Equiv), (int)(targets[i].MidZ / ZAxis.Equiv),
                            targets[i].Speed / XAxis.Equiv / 1000, _acc, 0, 0);
                        break;
                    case 3://IO操作
                        sRtn = gts.mc.GT_BufIO(XAxis.CardNo, crd, (ushort)gts.mc.MC_GPO, (ushort)(1 << targets[i].IOIndex), (ushort)((targets[i].OnOff ? 0 : 1) << targets[i].IOIndex), 0);
                        break;
                    case 5://延时
                        sRtn = gts.mc.GT_BufDelay(XAxis.CardNo, crd, (ushort)(targets[i].Delay * 1000), 0);
                        break;
                    default:
                        break;
                }
            }
            // 将前瞻缓存区中的数据压入控制器
            sRtn = gts.mc.GT_CrdData(XAxis.CardNo, crd, System.IntPtr.Zero, 0);
        }
        public static void AxisLnXYMove(AxisParm XAxis, AxisParm YAxis, List<M1Point> targets, short crd, double acc)
        {
            // 指令返回值
            short sRtn;
            int space;
            sRtn = gts.mc.GT_CrdClear(XAxis.CardNo, crd, 0);
            for (int i = 0; i < targets.Count; i++)
            {
                switch (targets[i].Type)
                {
                    case 0://直线普通定位
                        sRtn = gts.mc.GT_LnXY(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv), targets[i].Speed / XAxis.Equiv / 1000, acc, 0, 0);
                        //// 查询返回值是否成功
                        //if (0 != sRtn)
                        //{
                        //    do
                        //    {
                        //        // 查询运动缓存区空间，直至空间不为0
                        //        sRtn = gts.mc.GT_CrdSpace(XY.CardNo, crd, out space, 0);
                        //    } while (0 == space);
                        //    // 重新调用上次失败的插补指令
                        //    sRtn = gts.mc.GT_LnXY(XY.CardNo, crd, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), targets[i].Speed / XY.Equiv / 1000, acc, 0, 0);
                        //}
                        break;
                    case 1://直线精确定位
                        sRtn = gts.mc.GT_LnXYG0(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv), targets[i].Speed / XAxis.Equiv / 1000, acc, 0);                        
                        break;
                    case 2://圆弧定位
                        sRtn = gts.mc.GT_ArcXYR(XAxis.CardNo, crd, (int)(targets[i].X / XAxis.Equiv), (int)(targets[i].Y / YAxis.Equiv),
                            targets[i].Radius / XAxis.Equiv, (short)targets[i].CircleDir, targets[i].Speed / XAxis.Equiv / 1000, acc, 0, 0);
                        break;
                    case 3://IO操作
                        sRtn = gts.mc.GT_BufIO(XAxis.CardNo, crd, (ushort)gts.mc.MC_GPO, (ushort)(1 << targets[i].IOIndex), (ushort)((targets[i].OnOff ? 0 : 1) << targets[i].IOIndex), 0);
                        break;
                    case 5://延时
                        sRtn = gts.mc.GT_BufDelay(XAxis.CardNo, crd, (ushort)(targets[i].Delay * 1000), 0);
                        break;
                    default:
                        break;
                }
            }
            // 将前瞻缓存区中的数据压入控制器
            sRtn = gts.mc.GT_CrdData(XAxis.CardNo, crd, System.IntPtr.Zero, 0);
        }
        public static void AxisStartCrd(short cardNum, short crd)
        {
            gts.mc.GT_CrdStart(cardNum, (short)(1 << (crd - 1)), 0);
        }
        public static void AxisStop(short cardNum, int crdIndex, int type)
        {
            gts.mc.GT_Stop(cardNum, 1 << (8 + crdIndex - 1), type << (8 + crdIndex - 1));
        }
        public static bool AxisCheckCrdDone(short cardNum, short crd)
        {
            short run;
            int seg;
            gts.mc.GT_CrdStatus(cardNum, crd, out run, out seg, 0);
            return run != 1;
        }
        public static void ComparePulseTrigger(short cardNum, int hsioIndex)
        {
            short level = (short)(hsioIndex == 1 ? 1 : 2);
            gts.mc.GT_ComparePulse(cardNum, level, 0, 100);//HSIO0输出100us的脉冲
        }
        public static void SetComparePort(short cardNum,bool isgpo, int hsio0, int hsio1)
        {
            if (isgpo)
            {
                gts.mc.GT_SetDoBit(cardNum, gts.mc.MC_GPO, (short)(hsio0 + 1), 1);
                gts.mc.GT_SetDoBit(cardNum, gts.mc.MC_GPO, (short)(hsio1 + 1), 1);
            }
            gts.mc.GT_SetComparePort(cardNum,isgpo ? gts.mc.COMPARE_PORT_GPO : gts.mc.COMPARE_PORT_HSIO, (short)hsio0, (short)hsio1);
        }
        public static void AxisCompareUse1(short cardNum, short encoder, int[] Buf1)
        {
            //gts.mc.GT_CompareData(cardNum,
            //    encoder,
            //    1,
            //    0,
            //    0,
            //    100,
            //    Buf1,
            //    (short)Buf1.Length,
            //    null,
            //    0);
            gts.mc.GT_2DCompareClear(cardNum, 0);
            gts.mc.GT_2DCompareMode(cardNum, 0, gts.mc.COMPARE2D_MODE_1D);
            gts.mc.T2DComparePrm Prm;
            Prm.encx = 1;
            Prm.ency = encoder;
            Prm.maxerr = 30;
            Prm.outputType = 0;
            Prm.source = 1;
            Prm.startLevel = 0;
            Prm.threshold = 0;
            Prm.time = 500;
            gts.mc.GT_2DCompareSetPrm(cardNum, 0, ref Prm);
            gts.mc.T2DCompareData[] pBuf = new gts.mc.T2DCompareData[Buf1.Length];
            for (int i = 0; i < Buf1.Length; i++)
            {
                pBuf[i].py = Buf1[i];
                pBuf[i].px = 0;
            }
            gts.mc.GT_2DCompareData(cardNum, 0, (short)Buf1.Length, ref pBuf[0], 0);
            gts.mc.GT_2DCompareStart(cardNum, 0);
        }
        public static void AxisCompareUse2(short cardNum, short encoder, int[] Buf1)
        {

            //gts.mc.GT_CompareData(cardNum, 
            //    encoder, 
            //    1, 
            //    0, 
            //    0, 
            //    100, 
            //    null, 
            //    0, 
            //    Buf1, 
            //    (short)Buf1.Length);
            gts.mc.GT_2DCompareClear(cardNum, 1);
            gts.mc.GT_2DCompareMode(cardNum, 1, gts.mc.COMPARE2D_MODE_1D);
            gts.mc.T2DComparePrm Prm;
            Prm.encx = 1;
            Prm.ency = encoder;
            Prm.maxerr = 30;
            Prm.outputType = 0;
            Prm.source = 1;
            Prm.startLevel = 0;
            Prm.threshold = 0;
            Prm.time = 500;
            gts.mc.GT_2DCompareSetPrm(cardNum, 1, ref Prm);
            gts.mc.T2DCompareData[] pBuf = new gts.mc.T2DCompareData[Buf1.Length];
            for (int i = 0; i < Buf1.Length; i++)
            {
                pBuf[i].py = Buf1[i];
                pBuf[i].px = 0;
            }
            gts.mc.GT_2DCompareData(cardNum, 1, (short)Buf1.Length, ref pBuf[0], 0);
            gts.mc.GT_2DCompareStart(cardNum, 1);
        }
        public static void AxisCompare2DUse1(short cardNum, short x, short y, int[,] Buf1)
        {
            gts.mc.GT_2DCompareClear(cardNum, 0);
            gts.mc.GT_2DCompareMode(cardNum, 0, gts.mc.COMPARE2D_MODE_2D);
            gts.mc.T2DComparePrm Prm;
            Prm.encx = x;
            Prm.ency = y;
            Prm.maxerr = 300;
            Prm.outputType = 0;
            Prm.source = 1;
            Prm.startLevel = 0;
            Prm.threshold = 0;
            Prm.time = 500;
            gts.mc.GT_2DCompareSetPrm(cardNum, 0, ref Prm);
            int len = Buf1.GetUpperBound(0) + 1;
            gts.mc.T2DCompareData[] pBuf = new gts.mc.T2DCompareData[len];
            for (int i = 0; i < len; i++)
            {
                pBuf[i].py = Buf1[i, 1];
                pBuf[i].px = Buf1[i, 0];
            }
            gts.mc.GT_2DCompareData(cardNum, 0, (short)len, ref pBuf[0], 0);
            gts.mc.GT_2DCompareStart(cardNum, 0);
        }
        public static void AxisCompare2DUse2(short cardNum, short x, short y, int[,] Buf1)
        {
            gts.mc.GT_2DCompareClear(cardNum, 1);
            gts.mc.GT_2DCompareMode(cardNum, 1, gts.mc.COMPARE2D_MODE_2D);
            gts.mc.T2DComparePrm Prm;
            Prm.encx = x;
            Prm.ency = y;
            Prm.maxerr = 300;
            Prm.outputType = 0;
            Prm.source = 1;
            Prm.startLevel = 0;
            Prm.threshold = 0;
            Prm.time = 500;
            gts.mc.GT_2DCompareSetPrm(cardNum, 1, ref Prm);
            int len = Buf1.GetUpperBound(0) + 1;
            gts.mc.T2DCompareData[] pBuf = new gts.mc.T2DCompareData[len];
            for (int i = 0; i < len; i++)
            {
                pBuf[i].py = Buf1[i, 1];
                pBuf[i].px = Buf1[i, 0];
            }
            gts.mc.GT_2DCompareData(cardNum, 1, (short)len, ref pBuf[0], 0);
            gts.mc.GT_2DCompareStart(cardNum, 1);
        }
        public static double GetAdc(short adc)
        {
            double pValue; uint pClock;
            gts.mc.GT_GetAdc(0, adc, out pValue, 1, out pClock);
            return pValue;
        }

        public static double GetEnc(short cardNum, short encoder)
        {
            double pValue1;
            uint pClock;
            gts.mc.GT_GetEncPos(cardNum, encoder, out pValue1, 1, out pClock);
            return pValue1;
        }
        public static void SetEnc(short cardNum, short encoder, int pos)
        {
            gts.mc.GT_SetEncPos(cardNum, encoder, pos);
        }
        public static void SetGear(AxisParm master, AxisParm slave, int masterEven = 1, int slaveEven = 1, int masterSlope = 0)
        {
            gts.mc.GT_PrfGear(master.CardNo, slave.AxisId, 0);//设置从轴为gear模式
            gts.mc.GT_SetGearMaster(master.CardNo, slave.AxisId, master.AxisId, gts.mc.GEAR_MASTER_PROFILE, 0);//设置跟随主轴和模式
            gts.mc.GT_SetGearRatio(master.CardNo, slave.AxisId, masterEven, slaveEven, masterSlope);//设置齿轮比
            gts.mc.GT_GearStart(master.CardNo, 1 << (slave.AxisId - 1));//启动gear
        }
        public static void AxisPTMove(ref AxisParm _AxisParam, PTData[] data, short fifo)
        {
            double pos = 0;
            int time = 0;
            var r1 = gts.mc.GT_PrfPt(_AxisParam.CardNo, _AxisParam.AxisId, gts.mc.PT_MODE_STATIC);//设置静态PT运动模式
            r1 = gts.mc.GT_PtClear(_AxisParam.CardNo, _AxisParam.AxisId, fifo);//清空FIFO0
            for (int i = 0; i < data.Length; i++)
            {
                pos += (int)Math.Round(data[i].pos / _AxisParam.Equiv, 0);
                time += data[i].time;
                short type1 = gts.mc.PT_SEGMENT_NORMAL;
                if (i == data.Length - 1)
                {
                    type1 = gts.mc.PT_SEGMENT_STOP;
                }
                r1 = gts.mc.GT_PtData(_AxisParam.CardNo, _AxisParam.AxisId, pos, time, type1, fifo);
            }
            double pValue1;
            uint pClock;
            gts.mc.GT_GetEncPos(_AxisParam.CardNo, _AxisParam.AxisId, out pValue1, 1, out pClock);
            _AxisParam.Target = pValue1 + pos;
            int option = fifo;
            r1 = gts.mc.GT_PtStart(_AxisParam.CardNo, 1 << (_AxisParam.AxisId - 1), option << (_AxisParam.AxisId - 1));//启动FIFO的PT运动
        }
        public static short AxisPtSpace(AxisParm _AxisParam, short fifo)
        {
            gts.mc.GT_PtSpace(_AxisParam.CardNo, _AxisParam.AxisId, out var pSpace, fifo);
            return pSpace;
        }
        /// <summary>
        /// 获取PVT运动的数据，PT运动也能用。PVT采用percent描述方式，只是比PT运动多了指定加速度曲线的百分比。
        /// </summary>
        /// <param name="mdata">输入数据信息</param>
        /// <param name="acc">加速度</param>
        /// <param name="percent">加速度曲线百分比</param>
        /// <returns></returns>
        public static List<Tuple<double, double, double>> GetPVTData(double[,] mdata, double acc = 5000, double percent = 60)
        {
            /*
             * double[,] mdata = new double[,]
               {
                    { 0,300},//位移,最终速度(可能距离太短达不到)
                    { 59.106,10},
                    { 79.106,300},
                    { 138.211,10},
                    { 158.211,300},
                    { 217.3165,10},
                    { 237.3165,0},
               };
             */
            double percent1 = percent > 100 ? 100 : percent;
            percent1 = percent < 0 ? 0 : percent;
            List<Tuple<double, double, double>> outDataList = new List<Tuple<double, double, double>>();//Tuple:距离,时间,加速度百分比
            double preSpeed = 0;//上个节点的速度
            int arrCount = mdata.GetLength(0);
            for (int i = 0; i < arrCount; i++)
            {
                if (i < arrCount - 1)
                {
                    double accTime = Math.Abs(mdata[i, 1] - preSpeed) / acc;//加速时间
                    double accDist = (mdata[i, 1] + preSpeed) * accTime / 2;//加速距离。用梯形公式算面积

                    if (mdata[i + 1, 0] - mdata[i, 0] > accDist)
                    {
                        //有加速和匀速阶段
                        outDataList.Add(new Tuple<double, double, double>(accDist, accTime, percent1));
                        //Console.WriteLine("accDist:{0:F3},accTime:{1:F3}:speed:{2:F3}", accDist, accTime, mdata[i, 1]);

                        double avgDist = mdata[i + 1, 0] - mdata[i, 0] - accDist;//匀速距离
                        double avgTime = avgDist / mdata[i, 1];//匀速时间
                        outDataList.Add(new Tuple<double, double, double>(avgDist, avgTime, 0));
                        preSpeed = mdata[i, 1];
                        //Console.WriteLine("avgDist:{0:F3},avgTime:{1:F3}:speed:{2:F3}", avgDist, avgTime, mdata[i, 1]);
                    }
                    else
                    {
                        //没有匀速阶段
                        double accDist1 = mdata[i + 1, 0] - mdata[i, 0];
                        double speed1 = Math.Sqrt(accDist1 * 2 * acc * (mdata[i, 1] < preSpeed ? -1 : 1) + Math.Pow(preSpeed, 2)); //用梯形面积算速度
                        double accTime1 = Math.Abs(preSpeed - speed1) / acc;//加速时间
                        outDataList.Add(new Tuple<double, double, double>(accDist1, accTime1, percent));
                        preSpeed = speed1;
                        //Console.WriteLine("accDist:{0:F3},accTime:{1:F3}:speed:{2:F3}", accDist1, accTime1, speed1);
                    }
                }
                else
                {
                    //最后一段为减速停止段，会比目标位置多一段减速距离
                    double accTime = Math.Abs(0 - preSpeed) / acc;//加速时间
                    double accDist = Math.Abs(0 - preSpeed) * accTime / 2;//加速距离
                    outDataList.Add(new Tuple<double, double, double>(accDist, accTime, percent1));
                }
            }
            return outDataList;
        }
        /// <summary>
        /// PVT运动。位移包含加速和匀速段
        /// </summary>
        /// <param name="_AxisParam"></param>
        /// <param name="outDataList">1:位置(增量) 2:时间(增量) 3:百分比</param>
        /// <param name="tableId"></param>
        public static void AxisPVTMove(ref AxisParm _AxisParam, List<Tuple<double, double, double>> outDataList, short tableId = 1)
        {
            short sRtn;
            var arr = outDataList.ToArray();
            double[] time_y = new double[arr.Length + 1];
            double[] pos_y = new double[arr.Length + 1];
            double[] percent_y = new double[arr.Length + 1];
            sRtn = mc.GT_PrfPvt(_AxisParam.CardNo, _AxisParam.AxisId);
            double time = 0, pos = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                pos_y[i] = pos / _AxisParam.Equiv;
                time_y[i] = time * 1000;
                percent_y[i] = arr[i].Item3;
                pos += arr[i].Item1;
                time += arr[i].Item2;
            }
            pos_y[arr.Length] = pos / _AxisParam.Equiv;
            time_y[arr.Length] = time * 1000;
            percent_y[arr.Length] = 0;
            sRtn = mc.GT_PvtTablePercent(_AxisParam.CardNo, tableId, time_y.Length, ref time_y[0], ref pos_y[0], ref percent_y[0], 0);
            sRtn = mc.GT_PvtTableSelect(_AxisParam.CardNo, _AxisParam.AxisId, tableId);
            sRtn = mc.GT_PvtStart(_AxisParam.CardNo, 1 << (_AxisParam.AxisId - 1));
            double pValue1;
            uint pClock;
            mc.GT_GetEncPos(_AxisParam.CardNo, _AxisParam.AxisId, out pValue1, 1, out pClock);
            _AxisParam.Target = pValue1 + pos / _AxisParam.Equiv;
        }
    }
}
