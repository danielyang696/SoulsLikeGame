using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SG{
    public class PopUpManager : MonoBehaviour
    {
        [Header("You Died Pop UP")]
        [SerializeField] GameObject youDiedPopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedPopUpCanvasGroup;

        public void SendYouDiedPopUp(){
            youDiedPopUpGameObject.SetActive(true);
            youDiedPopUpText.characterSpacing = 0;

            StartCoroutine(StretchPopUpTextOverTime(youDiedPopUpText, 8f, 8.32f));
            StartCoroutine(FadeInPopUpOverTime(youDiedPopUpCanvasGroup, 5f));
            StartCoroutine(WaitTheFadeOutPopUpOverTime(youDiedPopUpCanvasGroup, 5f, 5f));
        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount){
            text.characterSpacing = 0; //reset text spacing
            if (duration > 0){
                float timer = 0f;
                yield return new WaitForSeconds(2f);

                while (timer <duration){
                    timer += Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20f));
                    yield return null;
                }
            }
        }

        private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration){
            if (duration > 0){
                canvas.alpha = 0f;
                float timer = 0f;
                yield return new WaitForSeconds(2f);

                while (timer < duration){
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                    yield return null;
                }
            }

            yield return null;
        }

        private IEnumerator WaitTheFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay){
            yield return new WaitForSeconds(2f);

            if (duration > 0){
                while (delay > 0){
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1f;
                float timer = 0f;

                yield return null;

                while (timer < duration){
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                    yield return null;
                }

                youDiedPopUpGameObject.SetActive(false);
                yield return null;
            }
        }
    }
}
