using System;
using System.Collections.Generic;
using System.Linq;

  public static class QuickSort
    {
        public static void sort(int[] arr, int left, int right)
        {
            int pivot, l_holder, r_holder;

            l_holder = left;
            r_holder = right;
            pivot = arr[left];

            while (left < right)
            {
                while ((arr[right] >= pivot) && (left < right))
                {
                    right--;
                }

                if (left != right)
                {
                    arr[left] = arr[right];
                    left++;
                }

                while ((arr[left] <= pivot) && (left < right))
                {
                    left++;
                }

                if (left != right)
                {
                    arr[right] = arr[left];
                    right--;
                }
            }

            arr[left] = pivot;
            pivot = left;
            left = l_holder;
            right = r_holder;

            if (left < pivot)
            {
                sort(arr,left, pivot - 1);
            }

            if (right > pivot)
            {
                sort(arr,pivot + 1, right);
            }
        }

        

 
    }

