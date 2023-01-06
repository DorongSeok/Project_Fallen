// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:6,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34084,y:33017,varname:node_4013,prsc:2|emission-2390-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9174,x:32206,y:32766,ptovrint:False,ptlb:Voronoi Seamless,ptin:_VoronoiSeamless,varname:node_9174,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2922,x:32206,y:32944,varname:node_2922,prsc:2,ntxv:0,isnm:False|UVIN-4945-UVOUT,TEX-9174-TEX;n:type:ShaderForge.SFN_Panner,id:4945,x:32027,y:32944,varname:node_4945,prsc:2,spu:2,spv:0.1|UVIN-7508-UVOUT,DIST-983-OUT;n:type:ShaderForge.SFN_TexCoord,id:7508,x:31801,y:32944,varname:node_7508,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:6328,x:32206,y:33099,varname:node_6328,prsc:2,ntxv:0,isnm:False|UVIN-8848-UVOUT,TEX-9174-TEX;n:type:ShaderForge.SFN_Panner,id:8848,x:32027,y:33099,varname:node_8848,prsc:2,spu:2.2,spv:-0.1|UVIN-8404-OUT,DIST-983-OUT;n:type:ShaderForge.SFN_RemapRange,id:8404,x:31801,y:33099,varname:node_8404,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.75|IN-7508-UVOUT;n:type:ShaderForge.SFN_Blend,id:9800,x:32424,y:33029,varname:node_9800,prsc:2,blmd:6,clmp:True|SRC-2922-R,DST-6328-R;n:type:ShaderForge.SFN_OneMinus,id:3406,x:32608,y:33029,varname:node_3406,prsc:2|IN-9800-OUT;n:type:ShaderForge.SFN_TexCoord,id:8150,x:31057,y:33391,varname:node_8150,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_OneMinus,id:5856,x:32027,y:33498,varname:node_5856,prsc:2|IN-2043-OUT;n:type:ShaderForge.SFN_Blend,id:5889,x:32252,y:33429,varname:node_5889,prsc:2,blmd:0,clmp:True|SRC-2043-OUT,DST-5856-OUT;n:type:ShaderForge.SFN_RemapRange,id:1334,x:32429,y:33429,varname:node_1334,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-5889-OUT;n:type:ShaderForge.SFN_Clamp01,id:4896,x:32604,y:33429,varname:node_4896,prsc:2|IN-1334-OUT;n:type:ShaderForge.SFN_Lerp,id:5404,x:32920,y:33226,varname:node_5404,prsc:2|A-3406-OUT,B-4896-OUT,T-9391-OUT;n:type:ShaderForge.SFN_Vector1,id:9391,x:32920,y:33373,varname:node_9391,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:2043,x:32027,y:33356,varname:node_2043,prsc:2|IN-3116-OUT;n:type:ShaderForge.SFN_RemapRange,id:6543,x:31286,y:33355,varname:node_6543,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:0|IN-8150-V;n:type:ShaderForge.SFN_RemapRange,id:150,x:31286,y:33514,varname:node_150,prsc:2,frmn:0,frmx:1,tomn:1,tomx:2|IN-8150-V;n:type:ShaderForge.SFN_Lerp,id:173,x:31497,y:33428,varname:node_173,prsc:2|A-6543-OUT,B-150-OUT,T-8605-R;n:type:ShaderForge.SFN_Tex2d,id:8605,x:31057,y:33560,varname:node_8605,prsc:2,ntxv:0,isnm:False|UVIN-9896-UVOUT,TEX-9174-TEX;n:type:ShaderForge.SFN_TexCoord,id:6597,x:30533,y:33560,varname:node_6597,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:6653,x:30715,y:33560,varname:node_6653,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.3|IN-6597-UVOUT;n:type:ShaderForge.SFN_Panner,id:9896,x:30887,y:33560,varname:node_9896,prsc:2,spu:0.5,spv:0.1|UVIN-6653-OUT,DIST-983-OUT;n:type:ShaderForge.SFN_RemapRange,id:8401,x:33141,y:33226,varname:node_8401,prsc:2,frmn:0,frmx:1,tomn:-5,tomx:6|IN-5404-OUT;n:type:ShaderForge.SFN_Clamp01,id:5465,x:33318,y:33226,varname:node_5465,prsc:2|IN-8401-OUT;n:type:ShaderForge.SFN_Multiply,id:5702,x:33411,y:33057,varname:node_5702,prsc:2|A-2498-OUT,B-9080-RGB,C-5465-OUT;n:type:ShaderForge.SFN_Vector1,id:2498,x:33411,y:32990,varname:node_2498,prsc:2,v1:3;n:type:ShaderForge.SFN_TexCoord,id:2103,x:31497,y:33576,varname:node_2103,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:3116,x:31738,y:33506,varname:node_3116,prsc:2|A-173-OUT,B-2103-V,T-2103-U;n:type:ShaderForge.SFN_Color,id:9080,x:33141,y:33072,ptovrint:False,ptlb:Energy Color,ptin:_EnergyColor,varname:node_9080,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4941177,c2:0.4941177,c3:1,c4:1;n:type:ShaderForge.SFN_Clamp01,id:9537,x:33590,y:33057,varname:node_9537,prsc:2|IN-5702-OUT;n:type:ShaderForge.SFN_Blend,id:2390,x:33765,y:33057,varname:node_2390,prsc:2,blmd:10,clmp:True|SRC-9537-OUT,DST-9537-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2880,x:31293,y:33056,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_2880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:4050,x:31293,y:32899,varname:node_4050,prsc:2;n:type:ShaderForge.SFN_Multiply,id:983,x:31528,y:32976,varname:node_983,prsc:2|A-4050-T,B-2880-OUT;proporder:9174-9080-2880;pass:END;sub:END;*/

Shader "Shader Forge/Energy" {
    Properties {
        _VoronoiSeamless ("Voronoi Seamless", 2D) = "white" {}
        _EnergyColor ("Energy Color", Color) = (0.4941177,0.4941177,1,1)
        _Speed ("Speed", Float ) = 1
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
            Blend One OneMinusSrcColor
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _VoronoiSeamless; uniform float4 _VoronoiSeamless_ST;
            uniform float4 _EnergyColor;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_4050 = _Time;
                float node_983 = (node_4050.g*_Speed);
                float2 node_4945 = (i.uv0+node_983*float2(2,0.1));
                float4 node_2922 = tex2D(_VoronoiSeamless,TRANSFORM_TEX(node_4945, _VoronoiSeamless));
                float2 node_8848 = ((i.uv0*0.75+0.0)+node_983*float2(2.2,-0.1));
                float4 node_6328 = tex2D(_VoronoiSeamless,TRANSFORM_TEX(node_8848, _VoronoiSeamless));
                float2 node_9896 = ((i.uv0*0.3+0.0)+node_983*float2(0.5,0.1));
                float4 node_8605 = tex2D(_VoronoiSeamless,TRANSFORM_TEX(node_9896, _VoronoiSeamless));
                float node_2043 = saturate(lerp(lerp((i.uv0.g*1.0+-1.0),(i.uv0.g*1.0+1.0),node_8605.r),i.uv0.g,i.uv0.r));
                float3 node_9537 = saturate((3.0*_EnergyColor.rgb*saturate((lerp((1.0 - saturate((1.0-(1.0-node_2922.r)*(1.0-node_6328.r)))),saturate((saturate(min(node_2043,(1.0 - node_2043)))*2.0+0.0)),0.5)*11.0+-5.0))));
                float3 emissive = saturate(( node_9537 > 0.5 ? (1.0-(1.0-2.0*(node_9537-0.5))*(1.0-node_9537)) : (2.0*node_9537*node_9537) ));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
