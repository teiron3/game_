using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

partial class form{
    //�摜���菈���p�̃N���X��錾���C���X�^���X������
    pic_hit p_hit = new pic_hit();

    //�}�E�X����p�̃N���X��錾���C���X�^���X������
    mouse_class mouse = new mouse_class();

    //�摜����v������ true ��Ԃ��A��v���Ȃ���� false ��Ԃ�
    bool pic_con(string str){
        bool flg = p_hit.pic_con(p_class[str]);
        if(flg){
            logwrite(str + "�̉摜����v���܂���");
        }else{
            logwrite(str + "�̉摜�͈�v���܂���");
        }
        return flg;
    }

    //�摜�𔭌������ true ��Ԃ��A�����ł��Ȃ���� false��Ԃ�
    bool pic_search(string str){
        bool flg = p_hit.pic_search(p_class[str]);
        if(flg){
            logwrite(str + "�̉摜�𔭌����܂���");
        }else{
            logwrite(str + "�̉摜�͔����ł��܂���ł���");
        }
        return flg;
    }

    void a_click(string str){
        mouse.before_move_and_click(p_class[str]);
    }

    //�摜��������܂ŃN���b�N
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
    //str1 ���Ȃ���� str2 ���N���b�N����
    //�摜��������Ȃ��ꍇ�A�G���[�t���O�𗧂Ă�
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

        for(int cnt = 0; cnt <= 30; cnt++){

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
            if(pic_con(str1)) return;
                
        }
        logwrite("error:" + p_class[str1].Name + "�̉摜��������܂���");
        stop_flg = true;
    }

    //�I�[�o�[���[�h(��ʂ̈ꕔ�ύX���m�L) 
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

    //�I�[�o�[���[�h str1 �̉ӏ����ω�����܂� str1 ���N���b�N
    void a_change_click(string str1){
        if(p_class[str1] == null ){
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

    //str1 �̉ӏ����ω�����܂� str2 ���N���b�N
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
    
    //str1 & str2 �̉ӏ����ω�����܂� str3 ���N���b�N
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

}