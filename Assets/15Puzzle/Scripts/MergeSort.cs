using UnityEngine;

public class MergeSort
{

    public static int inversionCount(int[] arr)
    {
        int[] tempArr = new int[arr.Length];
        for (int i = 0; i < arr.Length; ++i) tempArr[i] = arr[i];
        return MergeInversionCheck(tempArr, 0, tempArr.Length - 1);
    }

    public static int MergeInversionCheck(int[] arr, int l, int r)
    {
        int res = 0;
        if (l < r)
        {
            int m = (l + r) / 2;

            res += MergeInversionCheck(arr, l, m);
            res += MergeInversionCheck(arr, m + 1, r);

            res += CountandMerge(arr, l, m, r);   
        }
        return res;
    }

    public static int CountandMerge(int[] arr, int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int a = 0; a < n1; ++a) L[a] = arr[l + a];
        for (int b = 0; b < n2; ++b) R[b] = arr[m + 1 + b];

        int res = 0;
        int i = 0, j = 0, k = l;
        while (i < n1 && j < n2) {
            if (L[i] <= R[j]) {
                arr[k] = L[i];
                i++;
            } else {
                arr[k] = R[j];
                j++;
                res += (n1 - i);
            }
            k++;
        }

        while (i < n1) {
            arr[k] = L[i];
            i++; k++;
        }
        while (j < n2) {
            arr[k] = R[j];
            j++; k++;
        }

        return res;
    }

}
