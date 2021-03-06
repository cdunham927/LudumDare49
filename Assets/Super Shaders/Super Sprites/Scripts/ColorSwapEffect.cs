using UnityEngine;

namespace SuperShaders
{
    [ExecuteInEditMode]
    public class ColorSwapEffect : MonoBehaviour
    {
        [Header("Color A")]
        public Color sourceA = Color.blue;
        //public Color targetA = Color.red;

        [Header("Color B")]
        public Color sourceB = Color.blue;
        //public Color targetB = Color.red;

        [Header("Color C")]
        public Color sourceC = Color.blue;
        //public Color targetC = Color.red;

        [Range(0, 6)]
        public int curPallete;
        public Color[] pallete1;

        private SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            UpdateEffect(true);
        }

        private void OnDisable()
        {
            UpdateEffect(false);
        }

        private void Update()
        {
            UpdateEffect(true);
        }

        private void UpdateEffect(bool enable)
        {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();

            spriteRenderer.GetPropertyBlock(mpb);

            mpb.SetColor("_SourceA", enable ? sourceA : Color.blue);
            mpb.SetColor("_TargetA", enable ? pallete1[curPallete] : Color.red);

            mpb.SetColor("_SourceB", enable ? sourceB : Color.blue);
            mpb.SetColor("_TargetB", enable ? pallete1[curPallete] : Color.red);

            mpb.SetColor("_SourceC", enable ? sourceC : Color.blue);
            mpb.SetColor("_TargetC", enable ? pallete1[curPallete] : Color.red);

            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}
