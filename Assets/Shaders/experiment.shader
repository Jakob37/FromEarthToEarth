Shader "2D/Texture Only"
{  
    Properties
    {
        _MainTex ("Texture", 2D) = ""
    }
 
    SubShader
    {
        ZWrite On // "Off" might make more sense in very specific games

		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
		}

        Pass
        {
            SetTexture[_MainTex]
        }
    }
}