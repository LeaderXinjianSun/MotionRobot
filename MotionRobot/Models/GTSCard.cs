using System;
using System.Collections.Generic;

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
    public class M1Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Speed { get; set; }
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
        public static bool GetDi(short cardNum,ushort bitno)
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
            tHomePrm.acc = _axisParam.Acc /10;
            tHomePrm.dec = _axisParam.Acc /10;
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

        public static void AxisPosMove(ref AxisParm _AxisParam, double givePos, double speed = 0,double acc = 0)
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

        public static void AxisStop(short cardNum, int crdIndex, int type)
        {
            gts.mc.GT_Stop(cardNum, 1 << (8 + crdIndex - 1), type << (8 + crdIndex - 1));
        }

        public static bool SetCrd(short cardNum, int ZIndex)
        {
            gts.mc.TCrdPrm crdPrm;

            crdPrm.dimension = 3;                        // 建立三维的坐标系
            crdPrm.synVelMax = 1000;                      // 坐标系的最大合成速度是: 500 pulse/ms
            crdPrm.synAccMax = 20;                        // 坐标系的最大合成加速度是: 2 pulse/ms^2
            crdPrm.evenTime = 0;                         // 坐标系的最小匀速时间为0
            crdPrm.profile1 = 1;                       // 规划器1对应到X轴                       
            crdPrm.profile2 = 2;                       // 规划器2对应到Y轴
            crdPrm.profile3 = (short)(ZIndex == 1 ? 3 : 0);
            crdPrm.profile4 = (short)(ZIndex == 2 ? 3 : 0);
            crdPrm.profile5 = (short)(ZIndex == 3 ? 3 : 0);
            crdPrm.profile6 = (short)(ZIndex == 4 ? 3 : 0);
            crdPrm.profile7 = 0;
            crdPrm.profile8 = 0;
            crdPrm.setOriginFlag = 0;                    // 需要设置加工坐标系原点位置
            crdPrm.originPos1 = 0;                     // 加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
            crdPrm.originPos2 = 0;
            crdPrm.originPos3 = 0;
            crdPrm.originPos4 = 0;
            crdPrm.originPos5 = 0;
            crdPrm.originPos6 = 0;
            crdPrm.originPos7 = 0;
            crdPrm.originPos8 = 0;

            short SRtn = gts.mc.GT_SetCrdPrm(cardNum, 1, ref crdPrm);
            if (SRtn != 0)
            {
                return false;
            }
            return true;
        }

        public static void AxisLnXYZMove(AxisParm XY, AxisParm Z, List<M1Point> targets)
        {
            gts.mc.GT_CrdClear(XY.CardNo, 1, 0);
            for (int i = 0; i < targets.Count; i++)
            {
                switch (targets[i].Type)
                {
                    case 0://普通定位
                        gts.mc.GT_LnXYZ(XY.CardNo, 1, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), (int)(targets[i].Z / Z.Equiv), targets[i].Speed / XY.Equiv / 1000, 2, 0, 0);
                        break;
                    case 1://精确定位
                        gts.mc.GT_LnXYZG0(XY.CardNo, 1, (int)(targets[i].X / XY.Equiv), (int)(targets[i].Y / XY.Equiv), (int)(targets[i].Z / Z.Equiv), targets[i].Speed / XY.Equiv / 1000, 2, 0);
                        break;
                    case 2://延时
                        gts.mc.GT_BufDelay(XY.CardNo, 1, (ushort)targets[i].X, 0);
                        break;
                    default:
                        break;
                }
            }
            gts.mc.GT_CrdStart(XY.CardNo, 1, 0);
        }
        public static bool AxisCheckCrdDone(short cardNum)
        {
            short run;
            int seg;
            gts.mc.GT_CrdStatus(cardNum, 1, out run, out seg, 0);
            return run != 1;
        }
        public static void ComparePulseTrigger(short cardNum,int hsioIndex)
        {
            short level = (short)(hsioIndex == 1 ? 1 : 2);
            gts.mc.GT_ComparePulse(cardNum, level, 0, 100);//HSIO0输出100us的脉冲
        }

        public static void AxisCompareUse1(short cardNum, short encoder, int[] Buf1)
        {
   
            gts.mc.GT_CompareData(cardNum, 
                encoder, 
                1, 
                0, 
                0, 
                100, 
                Buf1, 
                (short)Buf1.Length,
                null, 
                0);
        }
        public static void AxisCompareUse2(short cardNum, short encoder, int[] Buf1)
        {
   
            gts.mc.GT_CompareData(cardNum, 
                encoder, 
                1, 
                0, 
                0, 
                100, 
                null, 
                0, 
                Buf1, 
                (short)Buf1.Length);
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
        public static void SetEnc(short cardNum, short encoder,int pos)
        {
            gts.mc.GT_SetEncPos(cardNum, encoder, pos);
        }
    }
}
