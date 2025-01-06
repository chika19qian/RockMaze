using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeStick : MonoBehaviour
{
    public int max_z; //フィールドの縦幅。5以上の奇数にすること。
    public int max_x; //フィールドの横幅。5以上の奇数にすること。
    public GameObject rockstarPrefab; // rockstarのプレハブ
    public GameObject RockStarPinkPrefab; // RockStarPinkのプレハブ
    int z; //フィールド配列の縦の要素番号
    int x; //フィールド配列の横の要素番号
    int r; //乱数の値
    Object wall; //壁オブジェクト
    GameObject wallgo; //壁のゲームオブジェクト
    List<Vector3> passagePositions = new List<Vector3>(); // 通路位置リスト
    public GameObject dialogPanel;

    void Start()
    {
        int[,] field = new int[max_z,max_x]; //フィールド（0が通路で、1が壁。）
        wall = Resources.Load("Wall"); //壁オブジェクトを読み込む。
        
        //通路（0）の生成
        for(z=0; z<max_z; z=z+1) //フィールドの縦幅の分だけループする。
        {
            for(x=0; x<max_x; x=x+1) //フィールドの横幅の分だけループする。
            {
                field[z,x] = 0;
            }
        }

        //上下の外壁（1）の生成
        for(x=0; x<max_x; x=x+1) //フィールドの横幅の分だけループする。
        {
            field[0,x] = 1;
            field[max_z-1,x] = 1;
        }

        //左右の外壁（1）の生成
        for(z=0; z<max_z; z=z+1) //フィールドの縦幅の分だけループする。
        {
            field[z,0] = 1;
            field[z,max_x-1] = 1;
        }

        //棒倒し法を使った壁（1）の生成（1行めのみ）
        z = 2; //1行め
        for(x=2; x<max_x-1; x=x+2) //要素番号xが2から要素番号max_x-1の値まで、1マス飛ばしで棒倒し。
        {
            r = Random.Range(1,13); //乱数生成（r = 1から12のランダムな値）
            field[z,x] = 1; //中心から……
            if(r<=3) //rが3以下のとき
            {
                if(field[z-1,x]==0) //上に棒（壁）がなければ
                {
                    field[z-1,x] = 1; //上に棒を倒す。
                }
                else if(field[z-1,x]==1) //上に棒（壁）があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                }
            }
            if(r>=4 && r<=6) //rが4から6のとき
            {
                if(field[z+1,x]==0) //下に棒（壁）がなければ
                {
                    field[z+1,x] = 1; //下に棒を倒す。
                }
                else if(field[z+1,x]==1) //下に棒（壁）があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                }
            }
            if(r>=7 && r<=9) //rが7から9のとき
            {
                if(field[z,x-1]==0) //左に棒（壁）がなければ
                {
                    field[z,x-1] = 1; //左に棒を倒す。
                }
                else if(field[z,x-1]==1) //左に棒（壁）があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                }
            }
            if(r>=10) //rが10以上のとき
            {
                if(field[z,x+1]==0) //右に棒（壁）がなければ
                {
                    field[z,x+1] = 1; //右に棒を倒す。
                }
                else if(field[z,x+1]==1) //右に棒（壁）があれば
                {
                    x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                }
            }
        }
        
        //棒倒し法を使った壁（1）の生成（2行め以降）
        for(z=4; z<max_z-1; z=z+2) //zの要素番号4から要素番号max_z-1まで、1マス飛ばしで棒倒し。
        {
            for(x=2; x<max_x-1; x=x+2) //xの要素番号2から要素番号max_x-1まで、1マス飛ばしで棒倒し。
            {
                r = Random.Range(1,13); //乱数生成（r = 1から12のランダムな値）
                field[z,x] = 1; //中心から……
                if(r<=4) //rが4以下のとき
                {  
                    if(field[z+1,x]==0) //下に棒（壁）がなければ
                    {
                        field[z+1,x] = 1; //下に棒を倒す。
                    }
                    else if(field[z+1,x]==1) //下に棒（壁）があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                    }
                }
                if(r>=5 && r<=8) //rが5から8のとき
                {
                    if(field[z,x-1]==0) //左に棒（壁）がなければ
                    {
                        field[z,x-1] = 1; //左に棒を倒す。
                    }
                    else if(field[z,x-1]==1) //左に棒（壁）があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                    }
                }
                if(r>=9) //rが9以上のとき
                {
                    if(field[z,x+1]==0) //右に棒（壁）がなければ
                    {
                        field[z,x+1] = 1; //右に棒を倒す。
                    }
                    else if(field[z,x+1]==1) //右に棒（壁）があれば
                    {
                        x = x - 2; //棒を倒さずに、乱数生成をやり直す。
                    }
                }
            }
        }

        field[0,1] = 0; //スタート地点の壁を撤去する。
        field[max_z-1,max_x-2] = 0; //ゴール地点の壁を撤去する。

        //壁の配置
        for(z=0; z<max_z; z=z+1) //フィールドの縦幅の分だけループする。
        {
            for(x=0; x<max_x; x=x+1) //フィールドの横幅の分だけループする。
            {
                if(field[z,x]==0) //通路なら
                {
                    passagePositions.Add(new Vector3(5.0f*x, 0.0f, 5.0f*z)); // 通路の位置を記録する
                }
                else if(field[z,x]==1) //壁なら
                {
                    wallgo = (GameObject)Instantiate(wall, new Vector3(5.0f*x,5.0f,5.0f*z), Quaternion.identity); //壁を配置する。
                }
            }
        }

        // 通路にrockstarをランダムに配置する
        PlaceRockstar();
    }

    void PlaceRockstar()
    {
        if (passagePositions.Count > 1) // 确保至少有两个位置
        {
            int randomIndex = Random.Range(0, passagePositions.Count);
            Vector3 randomPosition = passagePositions[randomIndex];

            // RockStarPinkをインスタンス化する
            Instantiate(RockStarPinkPrefab, randomPosition, Quaternion.identity);

            // 从列表中移除已使用的位置
            passagePositions.RemoveAt(randomIndex);

            // 生成第二个角色的位置
            randomIndex = Random.Range(0, passagePositions.Count);
            randomPosition = passagePositions[randomIndex];

            // rockstarをインスタンス化する
            Instantiate(rockstarPrefab, randomPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("通路の位置が見つかりませんでした。");
        }
    }

    
}
