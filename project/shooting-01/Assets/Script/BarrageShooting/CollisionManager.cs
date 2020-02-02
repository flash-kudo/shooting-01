using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BarrageShooting
{
    public enum CharacterTyep
    {
        NONE,
        SELF,
        ENEMY,
    }

    public class CollisionManager : MonoBehaviour
    {
        private const int SPLIT_V = 5 * 2;
        private const int SPLIT_H = 3 * 2;
        private const int CHARACTER_TYPE_COUNT = 3;
        private const float AREA_SIZE = 1.0f;

        private Vector2 AreaScale;
        private List<List<List<List<CharacterControll>>>> AreaLists;

        private static CollisionManager _Instance;
        /// *******************************************************
        /// <summary>Singleton参照</summary>
        /// *******************************************************
        public static CollisionManager Instance
        {
            get
            {
                if(_Instance == null) _Instance = (CollisionManager)FindObjectOfType(typeof(CollisionManager));
                return _Instance;
            }
        }

        /// *******************************************************
        /// <summary>初期処理</summary>
        /// *******************************************************
        void Awake()
        {
            _Instance = this;
            InitArea();
        }

        /// *******************************************************
        /// <summary>後期フレーム処理</summary>
        /// *******************************************************
        void LateUpdate()
        {
            HitCheckField();
            ClearArea();
        }

        /// *******************************************************
        /// <summary>エリア初期処理</summary>
        /// *******************************************************
        private void InitArea()
        {
            AreaScale = new Vector2(AREA_SIZE, AREA_SIZE);

            AreaLists = new List<List<List<List<CharacterControll>>>>();
            for(int h = 0; h < SPLIT_H; h++)
            {
                AreaLists.Add(new List<List<List<CharacterControll>>>());
                for(int v = 0; v < SPLIT_V; v++)
                {
                    AreaLists[h].Add(new List<List<CharacterControll>>());
                    for (int t = 0; t < CHARACTER_TYPE_COUNT; t++)
                    {
                        AreaLists[h][v].Add(new List<CharacterControll>());
                    }
                }
            }
            ClearArea();
        }

        /// *******************************************************
        /// <summary>エリア掃除</summary>
        /// *******************************************************
        private void ClearArea()
        {
            if (AreaLists == null) return;
            AreaLists.ForEach(v => v.ForEach(t => t.ForEach(c => c.Clear())));
        }

        /// *******************************************************
        /// <summary>当たり判定登録</summary>
        /// <param name="ctrl">登録アイテム</param>
        /// *******************************************************
        public void EntryHitCheck(CharacterControll ctrl)
        {
            Vector2Int areamin = GetPointArea(ctrl.BoundsMinX, ctrl.BoundsMinY);
            Vector2Int areamax = GetPointArea(ctrl.BoundsMaxX, ctrl.BoundsMaxY);

            for (int h = areamin.x; h <= areamax.x; h++)
            {
                for (int v = areamin.y; v <= areamax.y; v++)
                {
                    AddHitArea(ctrl, h, v, ctrl.CharType);
                }
            }
        }

        /// *******************************************************
        /// <summary>当たり判定エリア算出</summary>
        /// <param name="world_x">X座標</param>
        /// <param name="world_y">Y座標</param>
        /// <returns>エリア</returns>
        /// *******************************************************
        private Vector2Int GetPointArea(float world_x, float world_y)
        {
            return new Vector2Int(
                Mathf.FloorToInt(world_x / AreaScale.x) + (SPLIT_H / 2), 
                Mathf.FloorToInt(world_y / AreaScale.y) + (SPLIT_V / 2)
                );
        }

        /// *******************************************************
        /// <summary>エリア登録</summary>
        /// <param name="ctrl">対象コントロール</param>
        /// <param name="area_x">X座標</param>
        /// <param name="area_y">Y座標</param>
        /// <param name="char_type">キャラクタータイプ</param>
        /// *******************************************************
        private void AddHitArea(CharacterControll ctrl, int area_x, int area_y, CharacterTyep char_type)
        {
            if (area_x < 0) return;
            if (area_x >= SPLIT_H) return;
            if (area_y < 0) return;
            if (area_y >= SPLIT_V) return;
            int ctype = (int)char_type;
            if (ctype < 0) return;
            if (ctype >= CHARACTER_TYPE_COUNT) return;

            AreaLists[area_x][area_y][ctype].Add(ctrl);
        }

        /// *******************************************************
        /// <summary>フィールド内当たり判定処理</summary>
        /// *******************************************************
        private void HitCheckField()
        {
            if (AreaLists == null) return;
            for (int h = 0; h < SPLIT_H; h++)
            {
                for (int v = 0; v < SPLIT_V; v++)
                {
                    for (int t = 0; t < CHARACTER_TYPE_COUNT; t++)
                    {
                        HitCheckArea(AreaLists[h][v]);
                    }
                }
            }
        }

        /// *******************************************************
        /// <summary>エリア内当たり判定処理</summary>
        /// <param name="area">対象エリア</param>
        /// *******************************************************
        private void HitCheckArea(List<List<CharacterControll>> area)
        {
            area[(int)CharacterTyep.NONE].ForEach(src => {
                area[(int)CharacterTyep.SELF].ForEach(dst => {
                    HitCheckChar(src, dst);
                });
                area[(int)CharacterTyep.ENEMY].ForEach(dst => {
                    HitCheckChar(src, dst);
                });
            });
            area[(int)CharacterTyep.SELF].ForEach(src => {
                area[(int)CharacterTyep.ENEMY].ForEach(dst => {
                    HitCheckChar(src, dst);
                });
            });
        }

        /// *******************************************************
        /// <summary>キャラ同士当たり判定</summary>
        /// <param name="self">判定対象１</param>
        /// <param name="target">判定対象２</param>
        /// *******************************************************
        private void HitCheckChar(CharacterControll self, CharacterControll target)
        {
            if(self.HitTest(target) == true)
            {
                self.AddHitTarget(target);
                target.AddHitTarget(self);
            }
        }

    }
}
