
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

partial class form{

    ///<summary>test用</summary>
    void plantest(){
        dockflg = true;
        dockIn();
    }

    ///<summary>1-1周回(キラ付け)</summary>
    ///<remarks>1-1 → 遠征</remarks>
    void around1_1(){
        kirakan(gkantai, garoundcnt);if(stop_flg)return;
        expendjitiononly();
    }

    ///<summary>1-1周回のみ</summary>
    ///<remarks>遠征なし艦隊編成なし</remarks>
    ///編集途中
    void around1_1only(){
        for(int cnt = 0; cnt <= garoundcnt; cnt++){

        }
    }

    ///<summary>遠征のみ</summary>
    ///<remarks>遠征後に一定時間止まる</remarks>
    void expendjitiononly(){

        Func<int, int> rnd = (rr) => {
            Random r = new System.Random();
            return r.Next(rr);
        };
        for(int cnt = 0; cnt < 30; cnt++){
            //母港→出撃画面の遷移確認場所
            int portchangeX = 100; int portchangeY = 500;
            //動作開始
            //母港画面確認
            a_non_b_click("母港_出撃", "母港_母港"); if(stop_flg)return;
            //母港から海域選択画面に遷移
            a_non_b_click("出撃_出撃", "母港_出撃", portchangeX, portchangeY); if(stop_flg)return;
            //母港画面に戻る
            a_non_b_click("母港_出撃", "母港_母港");
            //遠征処理
            expedition(true);
            dockIn();
            Task.Delay(1800000 + rnd(2000000)).Wait();
        }
        logwrite("遠征オンリープラン終了");
    }

    ///<summary>キラ付け</summary>
    ///<param name="kantai">艦隊数</param>
    ///<param name="aroundcnt">周回数</param>
    ///<remarks>キラ付け1-1周回</remarks>
    ///<remarks>kantai は保存されている艦隊いくつやるかの数</remarks>
    void kirakan(int kantai, int aroundcnt){
        //kantai の数制限(error 防止)
        kantai = (kantai > 5) ? 5 : kantai;
        //aroundcnt の数の制限(キラの性質上3 以下に設定する)
        aroundcnt = (aroundcnt > 3) ? 3 : aroundcnt;

        //キラ付けループ
        //引数 kantai 以下の回数ループ
        for(int kantaicnt = 1; kantaicnt <= kantai; kantaicnt++){
            //kancnt キラ付けを行う艦
            for(int kancnt = 1; kancnt <= 6; kancnt++){
                
                while(true){
                    //stop_flg == true の時はフラグをリセットして最初から
                    //if(stop_flg){stop_flg = false;continue;}

                    //単艦設定
                    maketankan(kantaicnt, kancnt);if(stop_flg){stop_flg = false;continue;}
                    onejudge();if(stop_flg){stop_flg = false;continue;}
                    break;
                }
                //変更した艦が周回に出せない状態の時はスキップ
                if(continueflg){
                    continueflg = false;
                    continue;
                }
                for(int cnt = 1; cnt <= aroundcnt; cnt++){
                    while(true){
                        Fielde1_1();if(stop_flg){stop_flg = false;continue;}
                        expedition(true);if(stop_flg){stop_flg = false;continue;}
                        break;
                    }
                    //中破以上のときは次にいく
                    if(continueflg){
                        continueflg = false;
                        break;
                    }
                }
                dockIn();
            }

        }
    }
}