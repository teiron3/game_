
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

partial class form{

    ///<summary>test�p</summary>
    void plantest(){
        dockflg = true;
        dockIn();
    }

    ///<summary>1-1����(�L���t��)</summary>
    ///<remarks>1-1 �� ����</remarks>
    void around1_1(){
        kirakan(gkantai, garoundcnt);if(stop_flg)return;
        expendjitiononly();
    }

    ///<summary>1-1����̂�</summary>
    ///<remarks>�����Ȃ��͑��Ґ��Ȃ�</remarks>
    ///�ҏW�r��
    void around1_1only(){
        for(int cnt = 0; cnt <= garoundcnt; cnt++){

        }
    }

    ///<summary>�����̂�</summary>
    ///<remarks>������Ɉ�莞�Ԏ~�܂�</remarks>
    void expendjitiononly(){

        Func<int, int> rnd = (rr) => {
            Random r = new System.Random();
            return r.Next(rr);
        };
        for(int cnt = 0; cnt < 30; cnt++){
            //��`���o����ʂ̑J�ڊm�F�ꏊ
            int portchangeX = 100; int portchangeY = 500;
            //����J�n
            //��`��ʊm�F
            a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
            //��`����C��I����ʂɑJ��
            a_non_b_click("�o��_�o��", "��`_�o��", portchangeX, portchangeY); if(stop_flg)return;
            //��`��ʂɖ߂�
            a_non_b_click("��`_�o��", "��`_��`");
            //��������
            expedition(true);
            dockIn();
            Task.Delay(1800000 + rnd(2000000)).Wait();
        }
        logwrite("�����I�����[�v�����I��");
    }

    ///<summary>�L���t��</summary>
    ///<param name="kantai">�͑���</param>
    ///<param name="aroundcnt">����</param>
    ///<remarks>�L���t��1-1����</remarks>
    ///<remarks>kantai �͕ۑ�����Ă���͑�������邩�̐�</remarks>
    void kirakan(int kantai, int aroundcnt){
        //kantai �̐�����(error �h�~)
        kantai = (kantai > 5) ? 5 : kantai;
        //aroundcnt �̐��̐���(�L���̐�����3 �ȉ��ɐݒ肷��)
        aroundcnt = (aroundcnt > 3) ? 3 : aroundcnt;

        //�L���t�����[�v
        //���� kantai �ȉ��̉񐔃��[�v
        for(int kantaicnt = 1; kantaicnt <= kantai; kantaicnt++){
            //kancnt �L���t�����s����
            for(int kancnt = 1; kancnt <= 6; kancnt++){
                
                while(true){
                    //stop_flg == true �̎��̓t���O�����Z�b�g���čŏ�����
                    //if(stop_flg){stop_flg = false;continue;}

                    //�P�͐ݒ�
                    maketankan(kantaicnt, kancnt);if(stop_flg){stop_flg = false;continue;}
                    onejudge();if(stop_flg){stop_flg = false;continue;}
                    break;
                }
                //�ύX�����͂�����ɏo���Ȃ���Ԃ̎��̓X�L�b�v
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
                    //���j�ȏ�̂Ƃ��͎��ɂ���
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