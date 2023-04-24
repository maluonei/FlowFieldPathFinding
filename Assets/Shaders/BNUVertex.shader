Shader "Custom/BNUVertex" {
	Properties{
	  _MainTex("Texture", 2D) = "white" {}						//2D纹理数值
	  _Amount("Extrusion Amount", Range(0,0.1)) = 0.1			//膨胀系数数值
	}
		SubShader{
		  CGPROGRAM
		  #pragma surface surf Lambert vertex:vert					//表面着色器编译指令
		  struct Input {										//Input结构体
			float2 uv_MainTex;									//uv纹理坐标
		  };
		  float _Amount;										//定义膨胀系数属性
		  sampler2D _MainTex;								//定义2D纹理
		  void vert(inout appdata_base v) {						//顶点变换函数
			v.vertex.xyz += v.normal * _Amount;			//通过法线挤压实现充气的效果
		  }
		  void surf(Input IN, inout SurfaceOutput o) {				//表面着色器函数
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;	//从纹理提取颜色为漫反射颜色赋值
		  }
		  ENDCG
	  }
		  Fallback "Diffuse"												//降级着色器
}

