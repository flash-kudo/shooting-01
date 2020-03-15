// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-1870-OUT,alpha-585-OUT;n:type:ShaderForge.SFN_Multiply,id:1870,x:32538,y:32750,varname:node_1870,prsc:2|A-4791-OUT,B-9807-OUT,C-2166-OUT;n:type:ShaderForge.SFN_VertexColor,id:7509,x:31612,y:32766,varname:node_7509,prsc:2;n:type:ShaderForge.SFN_Multiply,id:585,x:32418,y:32965,varname:node_585,prsc:2|A-7218-OUT,B-7509-A;n:type:ShaderForge.SFN_Panner,id:7143,x:30621,y:32745,varname:node_7143,prsc:2,spu:1,spv:0|UVIN-5618-UVOUT,DIST-1260-OUT;n:type:ShaderForge.SFN_Panner,id:3967,x:30847,y:32745,varname:node_3967,prsc:2,spu:0,spv:1|UVIN-7143-UVOUT,DIST-6106-OUT;n:type:ShaderForge.SFN_TexCoord,id:4750,x:31084,y:32745,varname:node_4750,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_Tex2d,id:5948,x:31265,y:33483,ptovrint:False,ptlb:LoopTex,ptin:_LoopTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3339-OUT;n:type:ShaderForge.SFN_TexCoord,id:5618,x:29037,y:33758,varname:node_5618,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Panner,id:6280,x:30683,y:33477,varname:node_6280,prsc:2,spu:1,spv:0|UVIN-5618-UVOUT,DIST-1531-OUT;n:type:ShaderForge.SFN_Panner,id:8066,x:30892,y:33457,varname:node_8066,prsc:2,spu:0,spv:1|UVIN-6280-UVOUT,DIST-6145-OUT;n:type:ShaderForge.SFN_TexCoord,id:7745,x:31902,y:32009,varname:node_7745,prsc:2,uv:2,uaff:True;n:type:ShaderForge.SFN_Smoothstep,id:3972,x:31421,y:32855,varname:node_3972,prsc:2|A-6675-OUT,B-4750-Z,V-4054-R;n:type:ShaderForge.SFN_Slider,id:2911,x:30755,y:32436,ptovrint:False,ptlb:MaskGlow,ptin:_MaskGlow,varname:node_3488,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6675,x:31095,y:32560,varname:node_6675,prsc:2|A-2911-OUT,B-4750-Z;n:type:ShaderForge.SFN_Smoothstep,id:9701,x:31462,y:33264,varname:node_9701,prsc:2|A-5704-OUT,B-4750-W,V-5948-G;n:type:ShaderForge.SFN_Slider,id:7436,x:30857,y:33175,ptovrint:False,ptlb:LoopGlow,ptin:_LoopGlow,varname:node_1553,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5704,x:31197,y:33210,varname:node_5704,prsc:2|A-7436-OUT,B-4750-W;n:type:ShaderForge.SFN_Multiply,id:5782,x:31624,y:33091,varname:node_5782,prsc:2|A-3972-OUT,B-9701-OUT;n:type:ShaderForge.SFN_Set,id:9033,x:31828,y:33091,varname:MASK,prsc:2|IN-5782-OUT;n:type:ShaderForge.SFN_Get,id:7218,x:31232,y:32436,varname:node_7218,prsc:2|IN-9033-OUT;n:type:ShaderForge.SFN_Slider,id:4791,x:32148,y:32810,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_3765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Multiply,id:1757,x:32002,y:32674,varname:node_1757,prsc:2|A-3963-OUT,B-7509-RGB;n:type:ShaderForge.SFN_Add,id:4365,x:32002,y:32451,varname:node_4365,prsc:2|A-7509-RGB,B-6691-OUT;n:type:ShaderForge.SFN_OneMinus,id:3963,x:31547,y:32624,varname:node_3963,prsc:2|IN-7218-OUT;n:type:ShaderForge.SFN_Tex2d,id:4054,x:31139,y:32938,ptovrint:False,ptlb:MaskTex,ptin:_MaskTex,varname:node_4054,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1170-OUT;n:type:ShaderForge.SFN_Slider,id:6107,x:29962,y:33588,ptovrint:False,ptlb:Loop_USpeed,ptin:_Loop_USpeed,varname:node_6107,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Time,id:5641,x:29718,y:33301,varname:node_5641,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1531,x:30389,y:33583,varname:node_1531,prsc:2|A-5641-T,B-6107-OUT,C-7900-OUT;n:type:ShaderForge.SFN_Multiply,id:6145,x:30683,y:33699,varname:node_6145,prsc:2|A-5641-T,B-1993-OUT,C-8360-OUT;n:type:ShaderForge.SFN_Slider,id:1993,x:30273,y:33730,ptovrint:False,ptlb:Loop_VSpeed,ptin:_Loop_VSpeed,varname:_Loop_USpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Add,id:3339,x:31082,y:33457,varname:node_3339,prsc:2|A-8066-UVOUT,B-2345-OUT;n:type:ShaderForge.SFN_Append,id:4803,x:29295,y:33562,varname:node_4803,prsc:2|A-5618-Z,B-5618-W;n:type:ShaderForge.SFN_RemapRange,id:6708,x:29503,y:33785,varname:node_6708,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-5618-Z;n:type:ShaderForge.SFN_RemapRange,id:8684,x:29503,y:33961,varname:node_8684,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-5618-W;n:type:ShaderForge.SFN_Append,id:9807,x:32111,y:32030,varname:node_9807,prsc:2|A-7745-U,B-7745-V,C-7745-Z;n:type:ShaderForge.SFN_Set,id:9291,x:29530,y:33520,varname:firstUV,prsc:2|IN-4803-OUT;n:type:ShaderForge.SFN_Get,id:2345,x:30431,y:33169,varname:node_2345,prsc:2|IN-9291-OUT;n:type:ShaderForge.SFN_Set,id:196,x:29693,y:33828,varname:UV0_Z,prsc:2|IN-6708-OUT;n:type:ShaderForge.SFN_Set,id:2698,x:29693,y:33961,varname:UV0_W,prsc:2|IN-8684-OUT;n:type:ShaderForge.SFN_Get,id:7900,x:30109,y:33701,varname:node_7900,prsc:2|IN-196-OUT;n:type:ShaderForge.SFN_Get,id:8360,x:30431,y:33828,varname:node_8360,prsc:2|IN-2698-OUT;n:type:ShaderForge.SFN_Slider,id:8109,x:30014,y:32675,ptovrint:False,ptlb:Mask_USpeed,ptin:_Mask_USpeed,varname:node_8109,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:8160,x:29959,y:32793,ptovrint:False,ptlb:Mask_VSpeed,ptin:_Mask_VSpeed,varname:node_8160,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Multiply,id:1260,x:30446,y:32806,varname:node_1260,prsc:2|A-5641-T,B-8109-OUT;n:type:ShaderForge.SFN_Multiply,id:6106,x:30650,y:32904,varname:node_6106,prsc:2|A-5641-T,B-8160-OUT;n:type:ShaderForge.SFN_Add,id:1170,x:30936,y:32945,varname:node_1170,prsc:2|A-3967-UVOUT,B-2596-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:2596,x:30684,y:33065,ptovrint:False,ptlb:MaskRandom,ptin:_MaskRandom,varname:node_2596,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7664-OUT,B-2345-OUT;n:type:ShaderForge.SFN_Vector1,id:7664,x:30431,y:33065,varname:node_7664,prsc:2,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:2166,x:32198,y:32533,ptovrint:False,ptlb:Dark_or_Light,ptin:_Dark_or_Light,varname:node_2166,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4365-OUT,B-1757-OUT;n:type:ShaderForge.SFN_Slider,id:6241,x:31293,y:32105,ptovrint:False,ptlb:LightValue,ptin:_LightValue,varname:node_6241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6691,x:31771,y:32208,varname:node_6691,prsc:2|A-6241-OUT,B-8193-OUT;n:type:ShaderForge.SFN_Smoothstep,id:8193,x:31486,y:32206,varname:node_8193,prsc:2|A-2977-OUT,B-9328-OUT,V-7218-OUT;n:type:ShaderForge.SFN_Slider,id:2977,x:31075,y:32212,ptovrint:False,ptlb:VolMin,ptin:_VolMin,varname:node_2977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:9328,x:31232,y:32321,varname:node_9328,prsc:2,v1:1;proporder:4791-6241-2977-2166-2596-4054-2911-8109-8160-5948-7436-6107-1993;pass:END;sub:END;*/

Shader "HOLO/HOLO_ToonEffects_Blend" {
    Properties {
        _Value ("Value", Range(0, 5)) = 1
        _LightValue ("LightValue", Range(0, 1)) = 0
        _VolMin ("VolMin", Range(0, 1)) = 0
        [MaterialToggle] _Dark_or_Light ("Dark_or_Light", Float ) = 0
        [MaterialToggle] _MaskRandom ("MaskRandom", Float ) = 1
        _MaskTex ("MaskTex", 2D) = "white" {}
        _MaskGlow ("MaskGlow", Range(0, 1)) = 0
        _Mask_USpeed ("Mask_USpeed", Range(-5, 5)) = 0
        _Mask_VSpeed ("Mask_VSpeed", Range(-5, 5)) = 0
        _LoopTex ("LoopTex", 2D) = "white" {}
        _LoopGlow ("LoopGlow", Range(0, 1)) = 0
        _Loop_USpeed ("Loop_USpeed", Range(-5, 5)) = 0
        _Loop_VSpeed ("Loop_VSpeed", Range(-5, 5)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x psp2 n3ds 
            #pragma target 3.0
            uniform sampler2D _LoopTex; uniform float4 _LoopTex_ST;
            uniform float _MaskGlow;
            uniform float _LoopGlow;
            uniform float _Value;
            uniform sampler2D _MaskTex; uniform float4 _MaskTex_ST;
            uniform float _Loop_USpeed;
            uniform float _Loop_VSpeed;
            uniform float _Mask_USpeed;
            uniform float _Mask_VSpeed;
            uniform fixed _MaskRandom;
            uniform fixed _Dark_or_Light;
            uniform float _LightValue;
            uniform float _VolMin;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_5641 = _Time;
                float2 firstUV = float2(i.uv0.b,i.uv0.a);
                float2 node_2345 = firstUV;
                float2 node_1170 = (((i.uv0+(node_5641.g*_Mask_USpeed)*float2(1,0))+(node_5641.g*_Mask_VSpeed)*float2(0,1))+lerp( 1.0, node_2345, _MaskRandom ));
                float4 _MaskTex_var = tex2D(_MaskTex,TRANSFORM_TEX(node_1170, _MaskTex));
                float UV0_W = (i.uv0.a*0.5+0.5);
                float UV0_Z = (i.uv0.b*0.5+0.5);
                float2 node_3339 = (((i.uv0+(node_5641.g*_Loop_USpeed*UV0_Z)*float2(1,0))+(node_5641.g*_Loop_VSpeed*UV0_W)*float2(0,1))+node_2345);
                float4 _LoopTex_var = tex2D(_LoopTex,TRANSFORM_TEX(node_3339, _LoopTex));
                float MASK = (smoothstep( (_MaskGlow*i.uv1.b), i.uv1.b, _MaskTex_var.r )*smoothstep( (_LoopGlow*i.uv1.a), i.uv1.a, _LoopTex_var.g ));
                float node_7218 = MASK;
                float3 emissive = (_Value*float3(i.uv2.r,i.uv2.g,i.uv2.b)*lerp( (i.vertexColor.rgb+(_LightValue*smoothstep( _VolMin, 1.0, node_7218 ))), ((1.0 - node_7218)*i.vertexColor.rgb), _Dark_or_Light ));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_7218*i.vertexColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    ///CustomEditor "ShaderForgeMaterialInspector"
}
