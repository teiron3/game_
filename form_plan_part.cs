using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

partial class form{

    ///<summary>��`��ʂ���̕⋋</summary>
    ///<param name="oneop">�����I�y ���ǂ����B���󔻒�p</param>
    ///<returns>byte �����̔���p</returns>
    ///<returns>bool continuflg</returns>
    ///<remarks>��1�͑��̕⋋��ʂő�����m�F</remarks>
    ///<remarks>��2�`4�͑��ŕ⋋�̗L���Ŗ߂�l�̉��������ݒ�</remarks>
    byte supply(bool oneop){
        //��������p�߂�l byte�ϐ��̐ݒ�
        byte flg = 0;
        //�v�⋋���ǂ����̔���ꏊ
        int watchsupplyX = 791;
        int watchsupplyY = 259;

        //delegate ��`��ʂɖ߂�
        Action home_port_return = () =>{
            a_non_b_click("��`_�o��", "��`_��`");
        };
        
        Func<bool> color_check = () =>{
            //bitmap�N���X�̃C���X�^���X��
            Bitmap bitmap = new Bitmap(1,1);

            //�f�B�X�v���C�̔C�ӂ̓_�̎擾
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen( new Point(watchsupplyX, watchsupplyY), new Point( 0, 0),  bitmap.Size);
            g.Dispose();

            //Color�N���X�̃C���X�^���X��
            Color c = bitmap.GetPixel(0,0);
            if(c.B >= 5){
                return true;
            }else{
                return false;
            }
        };

        //deligate �⋋���{
        Action run_supplay = () =>{
            for(int i = 0; i <= 2; i++)a_click("�⋋_�S�⋋");
        };

        //����J�n
        //��`����⋋��ʂɑJ��
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return flg;
        a_non_b_click("�⋋_�R��", "��`_�⋋"); if(stop_flg)return flg;

        //1�͑��̃_���[�W�m�F
        //oneop == true �̂Ƃ��͈�͂̂�
        //oneope == false �̂Ƃ���6�͌���
        if(oneop){
            continueflg = onedockjudge();

        }else{
            damagejudge();
            dockcheck();
        }

        //1�͑��̕⋋
        if(color_check()){ run_supplay();}

        //2�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��2");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("2in");
            flg |= 4;
        }
        if(stop_flg)return flg;

        //3�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��3");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("3in");
            flg |= 2;
        }
        //4�͑��̕⋋
        a_b_change_c_click("�⋋_��r�ꏊ1", "�⋋_��r�ꏊ2", "�⋋_�͑��I��4");
        if(stop_flg)return flg;
        if(color_check()){
            run_supplay();
            logwrite("4in");
            flg |= 1;
        }

        home_port_return();
        logwrite("�⋋����");
        return flg;
    }

    ///<summary>����</summary>
    ///<param>�Ȃ�</param>
    ///<returns>�Ȃ�</returns>
    ///<remarks>�j�����Ă���͂��������</remarks>
    ///<remarks>(�ϋv�o�[�̐Ԃ̒l�� 100 �ȏ�)</remarks>
    ///<remarks>dockflg == false �̂Ƃ��͔�����</remarks>
    void dockIn(){
        //�h�b�N�t���O��false �̂Ƃ��͖߂�
        if(!dockflg){return;}

        //�h�b�N�̋󂫃t���O
        int dockemp = 0;
        //�󂫃h�b�N�̐�
        int dockempcnt = 0;
        //�����h�b�N�̔���ꏊ
        int x = 498;
        int[] y = {270, 392, 516, 638};

        //�͂̏��
        int kanx = 919;//�ϋv�o�[
        int statasx = 1101;//�C���}�[�N
        int[] kany = {211, 258, 302, 350, 395};

        //�����h�b�N�������� red < 60, green > 180, blue > 180
        int red = 60, green = 180, blue = 180;

        //�F���莮 �� �����h�b�N�N���b�N���画������܂ŕύX
        //�w��̉ӏ��� ���� �̐F�������false��Ԃ�
        //5�Ԗڂ̊͂������̎��� dockflg = false ��
        //�ォ�珇�Ɍ��Ă��������ł���͂��������
        Func<string, bool> docksearch = (str) => {
            string[] tmp = str.Split(',');
            if( (int.Parse(tmp[0]) < red) && (int.Parse(tmp[1]) > green) && (int.Parse(tmp[2]) > blue)){
                return false;
            }
            return true;
        };
        //��`���������ʂɑJ��
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return ;
        a_non_b_click("����_�������", "��`_����"); if(stop_flg)return ;

        //�h�b�N�̎g�p����
        Task<string> task1 = null, task2 = null, task3 = null, task4 = null;
        task1 = Task.Run(() => p_hit.bitcolor(x, y[0]));
        task2 = Task.Run(() => p_hit.bitcolor(x, y[1]));
        task3 = Task.Run(() => p_hit.bitcolor(x, y[2]));
        task4 = Task.Run(() => p_hit.bitcolor(x, y[3]));

        while(!task4.IsCompleted){Task.Delay(200).Wait();}
        while(!task3.IsCompleted){Task.Delay(200).Wait();}
        while(!task2.IsCompleted){Task.Delay(200).Wait();}
        while(!task1.IsCompleted){Task.Delay(200).Wait();}
        
        if(docksearch(task1.Result)){dockemp |= 32;dockempcnt++;}
        if(docksearch(task2.Result)){dockemp |= 16;dockempcnt++;}
        if(docksearch(task3.Result)){dockemp |= 8;dockempcnt++;}
        if(docksearch(task4.Result)){dockemp |= 4;dockempcnt++;}

        //�h�b�N���J���Ă���Ԃ��� dockflg = true �̂Ƃ�������������
        int bitconst = 32;
        //�I���h�b�N�p�ϐ�
        int selectdock = 1;
        while((dockempcnt > 0) && dockflg){
            //�h�b�N���J���Ă���Ƃ�
            if((dockemp & bitconst) > 0){
                //�������͂̃J�E���g
                int cnt = 0;
                //��������͂̏ォ��̐�
                int kan = 0;
                string[] str = new string[0];
                //�͑D�I�����J��
                a_non_b_click("����_�͑D�I��", "����_�h�b�N" + selectdock.ToString());

                //�ϋv�o�[�̊m�F
                //�����Ώۂ̏ꍇ cnt++ �ŏ��̑Ώۊ͂̏��Ԃ� kan �ɓ���
                //1st
                if(int.Parse(p_hit.bitcolor(kanx, kany[0]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[0]))){
                        kan = 1;
                        cnt++;
                    }
                } 
                //2nd
                if(int.Parse(p_hit.bitcolor(kanx, kany[1]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[1]))){
                        kan = (kan != 0) ? 2: kan;
                        cnt++;
                    }
                } 
                //3rd
                if(int.Parse(p_hit.bitcolor(kanx, kany[2]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[2]))){
                        kan = (kan != 0) ? 3: kan;
                        cnt++;
                    }
                } 
                //4th
                if(int.Parse(p_hit.bitcolor(kanx, kany[3]).Split(',')[0]) > 100){
                    if(docksearch(p_hit.bitcolor(statasx, kany[3]))){
                        kan = (kan != 0) ? 4: kan;
                        cnt++;
                    }
                } 
                //5th
                //dockflg�p
                if(int.Parse(p_hit.bitcolor(kanx, kany[4]).Split(',')[0]) > 100){
                    cnt++;
                } 
                //�Ώۂ��P�ȉ��̏ꍇ dockflg = false
                dockflg = (cnt <= 1) ? false : true;
                //�����������K�v�Ȋ͂��Ȃ���Δ�����
                if(cnt == 0){break;}

                //����������
                a_non_b_click("����_�����J�n","����_�͑D" + kan.ToString());
                a_non_b_click("����_�͂�", "����_�����J�n");
                a_change_click("����_�͂�");

                Task.Delay(1000).Wait();
            }
            //loop�ŏI����
            dockempcnt--;
            bitconst >>= 1;
            selectdock++;
        }
        //��`�ɖ߂�
        a_non_b_click("��`_�o��", "��`_��`");
    }

    ///<summary>����</summary>
    ///<param name="oneop">�L���t���������I�y�̂Ƃ� true</param>
    ///<returns>�Ȃ�</returns>
    ///<remarks>�⋋���ĉ����̗L�����m�F</remarks>
    void expedition(bool oneop){
        //�����͑��t���O
        byte flg = 0;
        //��`����o����ʂɑJ�ڂ���Ƃ��̉摜��r�ꏊ
        int portchangeX = 580;
        int portchangeY = 500;

        //�⋋���A�����͑��̊m�F
        flg = supply(oneop);
        //�����͑�����`�ɖ߂��ĂȂ������甲����
        if(flg == 0)return;

        //�ݒ�t�@�C���̗L���̊m�F
        if(!System.IO.File.Exists(@"expandition.ini")) {
            logwrite("error:�����ݒ�t�@�C��������܂���");
            stop_flg = true;
            return;
        }
        //�����ݒ�t�@�C���̓ǂݍ���
        byte[] expandition_data = new byte[6];
        FileStream stream = File.Open(@".\expandition.ini", FileMode.Open, FileAccess.Read);
        stream.Read(expandition_data, 0, 6);
        stream.Close();
        
        //������ʂ�
        //��`��ʊm�F
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        //��`����o����ʂɑJ��
        a_non_b_click("�o��_�o��", "��`_�o��", portchangeX, portchangeY); if(stop_flg)return;
        //������ʂɑJ��
        a_non_b_click("����_���","�o��_����"); if(stop_flg)return;

        //�����͑�����O�܂Ői�߂郁�\�b�h
        //�ϐ� i �͊͑� �ϐ� b �͑O�̊͑��̊C�� �߂�l�͏������s�����C��
        Func<int, int, int> sub_expendition = (i, b) => {
            //error process
            if((expandition_data[i] <= 0) || (expandition_data[i + 1] >= 9)){
                stop_flg = true;
                return 0;
            }
            string tmp_str1 = "�����C��_" + expandition_data[i].ToString();
            string tmp_str2 = "�����C��ڍ�_" + expandition_data[i + 1].ToString();
            if(expandition_data[i] != b){
                System.Threading.Tasks.Task.Delay(10000).Wait();
                a_change_click(tmp_str2, tmp_str1);
            }
            a_non_b_click("��������_�C�挈��", tmp_str2); 
            a_change_click("��������_�C�挈��");
            //a_non_b_click("��������_�����J�n", "��������_�C�挈��"); 

            return expandition_data[i];
        };

        //��������܂ŏ�������
        //�O�̊͑��̊C�� �����l��1
        int bf = 1;
        //��2�͑��̏���
        if((flg & 4) > 0){
            bf = sub_expendition(0, bf);if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��2�͑������o��");
        }
        //��3�͑��̏���
        if((flg & 2) > 0){
            bf = sub_expendition(2, bf);if(stop_flg)return;
            a_change_click("��������_�͑��I��3");if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��3�͑������o��");
        }
        //��4�͑��̏���
        if((flg & 1) > 0){
            bf = sub_expendition(4, bf);if(stop_flg)return;
            a_change_click("��������_�͑��I��4");if(stop_flg)return;
            a_non_b_click("����_���", "��������_�����J�n");if(stop_flg)return;
            logwrite("��4�͑������o��");
        }

        //��`��ʂɖ߂�
        a_non_b_click("��`_�o��", "��`_��`");
    }

    ///<summary>1-1�o��</summary>
    ///<remarks>1��o��</remarks>
    void Fielde1_1(){
        //��`���o����ʂ̑J�ڊm�F�ꏊ
        int portchangeX = 580; int portchangeY = 500;
        //�o����ʁ��C���ʂ̑J�ڊm�F�ꏊ
        int seaX = 275; int seaY = 583;
        //�C���ʁ��C�挈��̑J�ڊm�F�ꏊ
        int seadicisionX = 900; int seadicisionY = 600;
        //�C�挈�聨�͑�����̑J�ڊm�F�ꏊ
        int fleeddicisionX = 715; int fleeddicisionY = 633;
        //����J�n
        //��`��ʊm�F
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return;
        //��`����C��I����ʂɑJ��
        a_non_b_click("�o��_�o��", "��`_�o��", portchangeX, portchangeY); if(stop_flg)return;
        a_non_b_click("�o��_�C����", "�o��_�o��", seaX, seaY); if(stop_flg)return;
        a_non_b_click("�o������_�C�挈��", "�o���C��ڍ�_1", seadicisionX, seadicisionY); if(stop_flg)return;
        a_non_b_click("�o������_�o������", "�o������_�C�挈��", fleeddicisionX, fleeddicisionY); if(stop_flg)return;
        a_b_change_c_click("�o������_��r�ꏊ1","�o������_��r�ꏊ2","�o������_�o������");if(stop_flg)return;

        //debug cnt
        int dcnt = 0;
        //�C��퓬
        do{
            System.Threading.Thread.Sleep(200);
            while(pic_con("�퓬_���j��")){
                a_click("��`_��`");
                System.Threading.Thread.Sleep(800);
            }
            if(pic_con("�퓬_�I��")){
                while(!pic_con("�퓬_�i��")){
                    a_click("��`_��`");
                    System.Threading.Thread.Sleep(800);
                    if(pic_con("��`_�o��")){
                        return;
                    }
                }
                a_del_a_click("�퓬_�i��");
            }

            if(pic_con("�퓬_�i��")){
                logwrite("�i��");
                a_del_a_click("�퓬_�i��");
            }
            if(pic_con("�퓬_���")){
                a_del_a_click("�퓬_���");
            }

            if(stop_flg)return;
            logwrite(dcnt.ToString());
            dcnt++;
        }while(!pic_con("��`_�o��"));
    }

    ///<summary>�ϋv�̊m�F(��`���⋋)</summary>
    ///<returns>bool continueflg �ɓ���</returns>
    ///<remarks>�P�͂Ŋ͂̎���p�����m�F����</remarks>
    ///<remarks>��ɕύX����Ɏg�p</remarks>
    void onejudge(){
        //����J�n
        //��`����⋋��ʂɑJ��
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return ;
        a_non_b_click("�⋋_�R��", "��`_�⋋"); if(stop_flg)return ;
        //�͂̊m�F
        continueflg = onedockjudge();
        
        //��`�ɖ߂�
        a_non_b_click("��`_�o��", "��`_��`");
    }
}