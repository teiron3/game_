using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

partial class form{
    //�摜���菈���p�̃N���X��錾���C���X�^���X������
    pic_hit p_hit = new pic_hit();

    //�}�E�X����p�̃N���X��錾���C���X�^���X������
    mouse_class mouse = new mouse_class();

    ///<summary>
    ///str �̈ʒu�̉摜����v����� true ��Ԃ��A��v���Ȃ���� false ��Ԃ�
    ///</summary>
    bool pic_con(string str){
        bool flg = p_hit.pic_con(p_class[str]);
        /*if(flg){
            logwrite(str + "�̉摜����v���܂���");
        }else{
            logwrite(str + "�̉摜�͈�v���܂���");
        }*/
        return flg;
    }

    ///<summary>
    ///str �̉摜�𔭌������ true ��Ԃ��A�����ł��Ȃ���� false��Ԃ�
    ///</summary>
    bool pic_search(string str){
        bool flg = p_hit.pic_search(p_class[str]);
        /*if(flg){
            logwrite(str + "�̉摜�𔭌����܂���");
        }else{
            logwrite(str + "�̉摜�͔����ł��܂���ł���");
        }*/
        return flg;
    }

    ///<summary>
    ///str �̈ʒu�����N���b�N
    ///</summary>
    void a_click(string str){
        mouse.before_move_and_click(p_class[str]);
    }

    ///<summary>
    ///str �摜��������܂� str ���N���b�N
    ///</summary>
    void a_del_a_click(string str){
        if(p_class[str] == null){
            stop_flg = true;
            return;
        }

        while(p_hit.pic_con(p_class[str])){
            mouse.before_move_and_click(p_class[str]);
            mouse.move_cursor(p_class["��`_��`"]);
            System.Threading.Thread.Sleep(1500);
        }
    }

    ///<summary>
    ///str1 ���Ȃ���� str2 ���N���b�N����
    ///�摜��������Ȃ��ꍇ�A�G���[�t���O�𗧂Ă�
    ///</summary>
    void a_non_b_click(string str1,string str2){
        
        if(p_class[str1] == null || p_class[str2] == null){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);

        if(pic_con(str1)) return;

        //�ω���r�p�摜�擾
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].Pic_X;
        cmpclass.Pic_Y = p_class[str1].Pic_Y;
        cmpclass.Pic_Width = p_class[str1].Pic_Width;
        cmpclass.Pic_Height = p_class[str1].Pic_Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;
        for(int ccnt = 0; ccnt < 30; ccnt++){
            for(int cnt = 0; cnt <= 20; cnt++){
                if(stop_flg)return;

                if(flg){
                    a_click(str2);
                    mouse.move_cursor(p_class["��`_��`"]);
                }

                if(flg){
                    for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                        System.Threading.Thread.Sleep(400);
                        flg = p_hit.pic_con(cmpclass);
                    }
                }
                if(pic_con(str1)) return;
            }
            flg = true;
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜��������܂���");
        stop_flg = true;
    }

    ///<summary>
    ///�I�[�o�[���[�h(��ʂ̈ꕔ�ύX���m�L) 
    ///cmp_x, cmp_y �̈ʒu�̕ω����`�F�b�N
    ///</summary>
    void a_non_b_click(string str1,string str2,int cmp_x,int cmp_y){
        //�ω���r�p�摜�擾
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = cmp_x;
        cmpclass.Pic_Y = cmp_y;
        cmpclass.Pic_Width = 10;
        cmpclass.Pic_Height = 10;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;
        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;
            if(flg){
                a_click(str2);
                mouse.move_cursor(p_class["��`_��`"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(600);
                    flg = p_hit.pic_con(cmpclass);
                }
            }
            if(!flg){
                if(pic_con(str1)){
                     break;
                }
            }
        }
    }

    ///<summary>
    ///�I�[�o�[���[�h str1 �̉ӏ����ω�����܂� str1 ���N���b�N
    ///</summary>
    void a_change_click(string str1){
        if(p_class[str1] == null ){
            stop_flg = true;
            return;
        }

        Task.Delay(300).Wait();

        //�ω���r�p�摜�擾
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].X;
        cmpclass.Pic_Y = p_class[str1].Y;
        cmpclass.Pic_Width = p_class[str1].Width;
        cmpclass.Pic_Height = p_class[str1].Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str1);
                mouse.move_cursor(p_class["��`_��`"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    Task.Delay(400).Wait();
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜���ω����܂���");
        stop_flg = true;
    }

    ///<summary>
    ///�I�[�o�[���[�h x, y �̉ӏ�(10 * 10 pixel)���ω�����܂� str1 ���N���b�N
    ///</summary>
    void a_change_click(string str1, int x, int y){
        if(p_class[str1] == null ){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);

        //�ω���r�p�摜�擾
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = x;
        cmpclass.Pic_Y = y;
        cmpclass.Pic_Width = 10;
        cmpclass.Pic_Height = 10;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str1);
                mouse.move_cursor(p_class["��`_��`"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(400);
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜���ω����܂���");
        stop_flg = true;
    }

    ///<summary>
    ///str1 �̉ӏ����ω�����܂� str2 ���N���b�N
    ///</summary>
    void a_change_click(string str1, string str2){
        if(p_class[str1] == null || p_class[str2] == null){
            stop_flg = true;
            return;
        }

        System.Threading.Thread.Sleep(300);


        //�ω���r�p�摜�擾
        pic_data_class cmpclass = new pic_data_class();
        cmpclass.Pic_X = p_class[str1].X;
        cmpclass.Pic_Y = p_class[str1].Y;
        cmpclass.Pic_Width = p_class[str1].Width;
        cmpclass.Pic_Height = p_class[str1].Height;
        p_hit.pic_get(cmpclass);

        //screen comp flg set
        bool flg = true;

        for(int cnt = 0; cnt <= 30; cnt++){
            if(stop_flg)return;

            if(flg){
                a_click(str2);
                mouse.move_cursor(p_class["��`_��`"]);
            }

            if(flg){
                for(int subcnt = 0; flg && subcnt <= 5 ; subcnt++){
                    System.Threading.Thread.Sleep(600);
                    flg = p_hit.pic_con(cmpclass);
                }
            }else{
                return;
            }
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜���ω����܂���");
        stop_flg = true;
    }
    
    ///<summary>
    ///str1 �� str2 �̉ӏ����ω�����܂� str3 ���N���b�N
    ///</summary>
    void a_b_change_c_click(string str1, string str2, string str3){
        p_hit.pic_get(p_class[str1]);
        p_hit.pic_get(p_class[str2]) ;
        for(int i = 0; i <= 20; i++){
            mouse.before_move_and_click(p_class[str3]);
            if(!(pic_con(str1)) || !(pic_con(str2)))return;
        }
        logwrite("error:a_b_change_c_click3 ��ʂ��ω����܂���");
        stop_flg = true;
    }

    ///<summary>�j���󋵂̔���(�⋋���)
    ///�_���[�W����p�t���O int damageRed,damageOrange
    ///</summary>
    void damagejudge(){
        //�_���[�W����t���O�̏�����
        damageRed = 0;
        damageOrange = 0;
        //�_���[�W����p�̐F���l
        //red ��j���̗΂̒l(<100)
        //orange ���j�ȏ�̐Ԃ̒l(=>250)
        //blue ����Ȑ̒l�A�Ⴄ�ꍇ�͑�j����(>100)
        int red = 100, orange = 250,blue = 100;
        //�ϋv����p�̍��W(�ϋv�o�[)
        int x = 488;
        int[] y = {272, 350, 427, 504, 581, 657};
        //�͂̐�����p�̍��W�ƐF(�h������)
        int kanx = 713;
        int[] kany = {252, 330, 405, 483, 559, 635};
        //�͂̐����J�E���g����ϐ�
        int cnt = 0;
        //�Ԃ�l�̐F���l��������ϐ�
        string[] tmp = new string[3];
        
        for(; cnt <= 5; cnt++){
            tmp = p_hit.bitcolor(kanx, kany[cnt]).Split(',');
            //cnt�Ɋ͂��Ȃ���Δ��肪�I������܂ő҂��Ĕ���p���l��Get
            if(int.Parse(tmp[2]) >= 150){
                break;
            }
        }
        //�͂̑ϋv���������
        Action<int, Task<string>> taskcomplete = (bittmp, taskx) =>{
                    while(!taskx.IsCompleted)Task.Delay(1000).Wait();
                    tmp = taskx.Result.Split(',');
                    logwrite(taskx.Result);
                    if(int.Parse(tmp[0]) >= orange)damageOrange |= bittmp;
                    if(int.Parse(tmp[1]) < red || int.Parse(tmp[2]) > blue)damageRed |= bittmp;
        };
        Task<string> task1, task2, task3, task4, task5, task6;
        task1 = Task.Run(() => p_hit.bitcolor30(x, y[0]));
        logwrite("1");
        if(cnt > 1){
             task2 = Task.Run( () => p_hit.bitcolor30(x, y[1]));
        logwrite("2");
            if(cnt > 2){
                task3 = Task.Run( () => p_hit.bitcolor30(x, y[2]));
        logwrite("3");
                if(cnt > 3){
                    task4 = Task.Run( () => p_hit.bitcolor30(x, y[3]));
        logwrite("4");
                    if(cnt > 4){
                        task5 = Task.Run( () => p_hit.bitcolor30(x, y[4]));
        logwrite("5");
                        if(cnt > 5){
                            task6 = Task.Run( () => p_hit.bitcolor30(x, y[5]));
        logwrite("6");
                            taskcomplete(1, task6);
                        }
                        taskcomplete(2, task5);
                    }
                    taskcomplete(4, task4);
                }
                taskcomplete(8, task3);
            }
            taskcomplete(16, task2);
        }
        taskcomplete(32, task1);

        if(damageRed > 0)logwrite("��j�͂���");
        if(damageOrange > 0)logwrite("���j�ȏ�̊͂���");
    }

    ///<summary>�����`�F�b�N(�⋋)
    ///�_���[�W����p�t���O int damageRed,damageOrange
    ///�����p�t���O bool dockflg
    ///�����̕K�v�Ȋ͂������ dockflg �� true �ɂ���
    ///</summary>
    void dockcheck(){
        if(damageOrange == 0){return;}
        //�������̔���ݒ�F(red < 60, green >180, blue > 190)
        int red = 60, green = 180, blue = 190;
        //����̏ꏊ
        int x = 454;
        int[] y = {277, 351, 429, 507, 585, 661};

        Task<string> task1 = null, task2 = null, task3 = null, task4 = null, task5 = null, task6 = null;
        if((damageOrange & 32) > 0){
            task1 = Task.Run(() => p_hit.bitcolor30(x, y[0]));
        }
        if((damageOrange & 16) > 0){
            task2 = Task.Run(() => p_hit.bitcolor30(x, y[1]));
        }
        if((damageOrange & 8) > 0){
            task3 = Task.Run(() => p_hit.bitcolor30(x, y[2]));
        }
        if((damageOrange & 4) > 0){
            task4 = Task.Run(() => p_hit.bitcolor30(x, y[3]));
        }
        if((damageOrange & 2) > 0){
            task5 = Task.Run(() => p_hit.bitcolor30(x, y[4]));
        }
        if((damageOrange & 1) > 0){
            task6 = Task.Run(() => p_hit.bitcolor30(x, y[5]));
        }
        //�͂������������肷��
        //�������łȂ���� dockflg = true
        Action<Task<string>> taskcomplete = (taskx) =>{
                    while(!taskx.IsCompleted)Task.Delay(1000).Wait();
                    string[] tmp = taskx.Result.Split(',');
                    logwrite(taskx.Result);
                    if(int.Parse(tmp[0]) > red && int.Parse(tmp[1]) < green && int.Parse(tmp[2]) < blue){
                        dockflg = true;
                    } 
        };
        if((damageOrange & 32) > 0){
            while(!task1.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task1);
        }
        if((damageOrange & 16) > 0){
            while(!task2.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task2);
        }
        if((damageOrange & 8) > 0){
            while(!task3.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task3);
        }
        if((damageOrange & 4) > 0){
            while(!task4.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task4);
        }
        if((damageOrange & 2) > 0){
            while(!task5.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task5);
        }
        if((damageOrange & 1) > 0){
            while(!task6.IsCompleted){Task.Delay(500).Wait();}
            taskcomplete(task6);
        }
    }

    ///<summary>
    ///�P�͐ݒ�
    ///int groupnum �͑�
    ///int kannum ��
    ///</summary>
    void maketankan(int groupnum, int kannum){
        //�O���͂̃J�E���g�p�ϐ�
        int tmp = kannum;
        //��`�m�F
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return ;
        //��`���Ґ����
        a_non_b_click("�Ґ�_���", "��`_�Ґ�"); if(stop_flg)return ;
        //�Ґ��W�J��ʂ�
        a_change_click("�Ґ�_�Ґ��W�J", 193, 641);
        //�Ґ��W�J
        a_change_click("�Ґ��W�J_�W�J" + groupnum.ToString(), 197, 215);

        while(tmp > 1){
            a_non_b_click("�Ґ�_�͑D�I��", "�Ґ�_�ύX(����)");
            a_change_click("�Ґ�_�͑D�I��", "�Ґ�_�O��");
            tmp--;
        }

        a_non_b_click("�Ґ�_��̑�2��", "�Ґ�_���͈ȊO�O��");

        //��`�ɖ߂�
        a_non_b_click("��`_�o��", "��`_��`"); if(stop_flg)return ;

    }

}