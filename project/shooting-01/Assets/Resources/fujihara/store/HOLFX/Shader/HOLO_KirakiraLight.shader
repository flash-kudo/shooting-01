// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31677,y:32163,varname:_MainTex,prsc:2,tex:af823242f44a5704fa46b1784d4ec5d7,ntxv:0,isnm:False|UVIN-1764-OUT,TEX-9503-TEX;n:type:ShaderForge.SFN_Multiply,id:2393,x:32240,y:32390,varname:node_2393,prsc:2|A-4606-OUT,B-2053-RGB,C-2053-A;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31967,y:32537,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:5421,x:31677,y:32365,varname:_MainTex_copy,prsc:2,tex:af823242f44a5704fa46b1784d4ec5d7,ntxv:0,isnm:False|UVIN-6330-OUT,TEX-9503-TEX;n:type:ShaderForge.SFN_Tex2d,id:2509,x:31677,y:32567,varname:_MainTex_copy_copy,prsc:2,tex:af823242f44a5704fa46b1784d4ec5d7,ntxv:0,isnm:False|UVIN-9343-OUT,TEX-9503-TEX;n:type:ShaderForge.SFN_TexCoord,id:2532,x:31237,y:32077,varname:node_2532,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1764,x:31480,y:32163,varname:node_1764,prsc:2|A-2532-UVOUT,B-327-OUT;n:type:ShaderForge.SFN_Multiply,id:6330,x:31480,y:32365,varname:node_6330,prsc:2|A-2532-UVOUT,B-3018-OUT;n:type:ShaderForge.SFN_Multiply,id:9343,x:31439,y:32567,varname:node_9343,prsc:2|A-2532-UVOUT,B-2262-OUT;n:type:ShaderForge.SFN_Vector2,id:731,x:31021,y:32568,varname:node_731,prsc:2,v1:-1,v2:1;n:type:ShaderForge.SFN_Vector2,id:1943,x:31021,y:32429,varname:node_1943,prsc:2,v1:1,v2:-1;n:type:ShaderForge.SFN_Vector2,id:8934,x:31021,y:32216,varname:node_8934,prsc:2,v1:1,v2:1;n:type:ShaderForge.SFN_Slider,id:3102,x:30238,y:32620,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_3102,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.222222,max:5;n:type:ShaderForge.SFN_RemapRange,id:3078,x:30615,y:32649,varname:node_3078,prsc:2,frmn:0,frmx:5,tomn:0,tomx:0.05|IN-3102-OUT;n:type:ShaderForge.SFN_Multiply,id:2452,x:30825,y:32342,varname:node_2452,prsc:2|A-5066-OUT,B-3078-OUT;n:type:ShaderForge.SFN_Multiply,id:622,x:30837,y:32502,varname:node_622,prsc:2|A-2112-OUT,B-3078-OUT;n:type:ShaderForge.SFN_Multiply,id:2458,x:30825,y:32649,varname:node_2458,prsc:2|A-1532-OUT,B-3078-OUT;n:type:ShaderForge.SFN_Vector2,id:5066,x:30600,y:32269,varname:node_5066,prsc:2,v1:1,v2:0;n:type:ShaderForge.SFN_Vector2,id:2112,x:30600,y:32385,varname:node_2112,prsc:2,v1:0,v2:-1;n:type:ShaderForge.SFN_Vector2,id:1532,x:30600,y:32480,varname:node_1532,prsc:2,v1:-1,v2:1;n:type:ShaderForge.SFN_Add,id:327,x:31221,y:32261,varname:node_327,prsc:2|A-8934-OUT,B-2452-OUT;n:type:ShaderForge.SFN_Add,id:3018,x:31204,y:32441,varname:node_3018,prsc:2|A-1943-OUT,B-622-OUT;n:type:ShaderForge.SFN_Add,id:2262,x:31218,y:32581,varname:node_2262,prsc:2|A-731-OUT,B-2458-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9503,x:31439,y:32773,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_9503,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:af823242f44a5704fa46b1784d4ec5d7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:4606,x:31967,y:32388,varname:node_4606,prsc:2|A-6074-R,B-5421-G,C-2509-B;proporder:3102-9503;pass:END;sub:END;*/

Shader "HOLO/HOLO_KirakiraLight" {
    Properties {
        _Distortion ("Distortion", Range(0, 5)) = 2.222222
        _MainTexture ("Main Texture", 2D) = "white" {}
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
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 
            #pragma target 3.0
            uniform float _Distortion;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_3078 = (_Distortion*0.01+0.0);
                float2 node_1764 = (i.uv0*(float2(1,1)+(float2(1,0)*node_3078)));
                float4 _MainTex = tex2D(_MainTexture,TRANSFORM_TEX(node_1764, _MainTexture));
                float2 node_6330 = (i.uv0*(float2(1,-1)+(float2(0,-1)*node_3078)));
                float4 _MainTex_copy = tex2D(_MainTexture,TRANSFORM_TEX(node_6330, _MainTexture));
                float2 node_9343 = (i.uv0*(float2(-1,1)+(float2(-1,1)*node_3078)));
                float4 _MainTex_copy_copy = tex2D(_MainTexture,TRANSFORM_TEX(node_9343, _MainTexture));
                float3 emissive = (float3(_MainTex.r,_MainTex_copy.g,_MainTex_copy_copy.b)*i.vertexColor.rgb*i.vertexColor.a);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    //CustomEditor "ShaderForgeMaterialInspector"
}
