using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
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

        [SerializeField]
        public Camera GameCamera;
        private Camera FieldCamera { get { return (GameCamera == null) ? Camera.main : GameCamera; } }

        private Vector2 AreaScale;
        private List<List<List<List<CharacterControll>>>> AreaLists;

        private static CollisionManager _Instance;
        public static CollisionManager Instance
        {
            get
            {
                if(_Instance == null) _Instance = (CollisionManager)FindObjectOfType(typeof(CollisionManager));
                return _Instance;
            }
        }

        void Awake()
        {
            _Instance = this;
            InitArea();
        }

        void LateUpdate()
        {
            HitCheckField();
            ClearArea();
        }

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

        private void ClearArea()
        {
            AreaLists.ForEach(v => v.ForEach(t => t.ForEach(c => c.Clear())));
        }
        public void EntryHitCheck(CharacterControll ctrl)
        {
            if (ctrl.Collider == null) return;

            Vector3 min = ctrl.Collider.bounds.min + ctrl.WorldPosition;
            Vector3 max = ctrl.Collider.bounds.max + ctrl.WorldPosition;

            Vector2Int areamin = GetPointArea(min.x, min.y);
            Vector2Int areamax = GetPointArea(max.x, max.y);

            for(int h = areamin.x; h <= areamax.x; h++)
            {
                for (int v = areamin.y; v <= areamax.y; v++)
                {
                    AddHitArea(ctrl, h, v, ctrl.CharType);
                }
            }
        }
        private Vector2Int GetPointArea(float world_x, float world_y)
        {
            return new Vector2Int(
                Mathf.FloorToInt(world_x / AreaScale.x) + (SPLIT_H / 2), 
                Mathf.FloorToInt(world_y / AreaScale.y) + (SPLIT_V / 2)
                );
        }
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

        private void HitCheckField()
        {
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

        private void HitCheckChar(CharacterControll self, CharacterControll target)
        {
            if(self.Collider.IsTouching(target.Collider) == true)
            {
                self.AddHitTarget(target);
                target.AddHitTarget(self);
            }
        }

    }
}
