using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<int> Key = new List<int>();

            for (int randKey1 = 0; randKey1 < 26; randKey1++)
            {
                for (int randKey2 = 0; randKey2 < 26; randKey2++)
                {
                    for(int randKey3 = 0;randKey3 < 26; randKey3++)
                    {
                        for (int randKey4 = 0; randKey4 < 26; randKey4++)
                        {

                            if (((randKey1 * plainText[0]) + (randKey2 * plainText[1])) % 26 == cipherText[0] )
                            {
                                if(((randKey1 * plainText[2]) + (randKey2 * plainText[3])) % 26 == cipherText[2])
                                {
                                    if(((randKey3 * plainText[0]) + (randKey4 * plainText[1])) % 26 == cipherText[1])
                                    {
                                        if(((randKey3 * plainText[2]) + (randKey4 * plainText[3])) % 26 == cipherText[3])
                                        {
                                            Key.Add(randKey1);
                                            Key.Add(randKey2);
                                            Key.Add(randKey3);
                                            Key.Add(randKey4);

                                        }
                                    }
                                }
                            }

                        }
                    }

                }
            }
            if (Key.Count < 4)
            {
                throw new InvalidAnlysisException();
            }

            return Key;
        }



        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<int> plain = new List<int>();
            int KM = (int)Math.Sqrt(key.Count());
            int[,] new_mat = new int[KM, KM];

            //key matrix
            int[,] mat_key = new int[KM, KM];

            for (int i = 0; i < KM; i++)
            {
                for (int j = 0; j < KM; j++)
                {
                    mat_key[i, j] = key[i * KM + j];
                }
            }

            //cipher matrix 
            int PM = cipherText.Count() / KM;
            int rows = KM;
            int cols = PM;

            int[,] mat_cipher = new int[rows, cols];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    mat_cipher[j, i] = cipherText[i * rows + j];
                }
            }

            //get B 
            int determinant = GetDeterminant(mat_key, KM);
            determinant = determinant % 26;
            if (determinant< 0)
            {
                determinant +=26 ;
            }
            int B = 0;
            for (int i = 2; i < 26; i++)
            {
                if (((i * determinant) % 26) == 1)
                {
                    B = i;
                    break;
                }
            }


            if (KM == 2)
            {

                int x = mat_key[0, 0] * mat_key[1, 1] - mat_key[1, 0] * mat_key[0, 1];

                x = 1 / x;

                int temp = mat_key[0, 0];

                mat_key[0, 0] = mat_key[1, 1];
                mat_key[1, 1] = temp;

                mat_key[0,1] *= -1*x ;
                mat_key[1,0] *= -1*x ;
                mat_key[0,0] *= x;
                mat_key[1,1] *= x;

                new_mat = mat_key;
            }



            else
            {

                // Find D 
            

                for (int i = 0; i < KM; i++)
                {
                    for (int j = 0; j < KM; j++)
                    {
                        List<int> tmp = new List<int>();
                        for (int k = 0; k < KM; k++)
                        {
                            for (int l = 0; l < KM; l++)
                            {
                                if (k != i && l != j)
                                {
                                    tmp.Add(mat_key[k, l]);
                                }
                            }
                        }

                        int res = tmp[0] * tmp[3] - tmp[1] * tmp[2];

                        new_mat[i, j] = res;
                    }
                }


                //power of 1 in the equation 
                for (int i = 0; i < KM; i++)
                {
                    for (int j = 0; j < KM; j++)
                    {
                        if ((i + j) % 2 != 0)
                        {
                            new_mat[i, j] *= -1;
                        }
                    }
                }

                for (int i = 0; i < KM; i++)
                    for (int j = 0; j < KM; j++)
                    {
                        new_mat[i, j] = (new_mat[i, j] * B) % 26;
                        if (new_mat[i, j] < 0)
                        {
                            new_mat[i, j] += 26;
                        }
                    }

                new_mat = matrix_Trans(new_mat, KM);
            }



            // Multiply the matrices
            int[,] result = new int[KM, PM];

            for(int i = 0; i < KM; i++)
            {
                for (int j = 0; j < PM; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < KM; k++)
                    {
                        sum += new_mat[i, k] * mat_cipher[k, j];
                    }
                    result[i, j] = sum % 26;
                    if (result[i, j] < 0)
                    {
                        result[i, j] += 26;
                    }
                }
            }

            for (int i = 0; i < PM; i++)
            {
                for (int j = 0; j < KM; j++)
                {
                    plain.Add(result[j, i]);
                }
            }

            if (plain.FindAll(s => s.Equals(0)).Count == plain.Count)
                throw new System.Exception();


            return plain;
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int KM = (int)Math.Sqrt(key.Count());

            int[,] mat_key = new int[KM, KM];

            for (int i = 0; i < KM; i++)
            {
                for (int j = 0; j < KM; j++)
                {
                    mat_key[i, j] = key[i * KM + j];
                }
            }


            int PM = plainText.Count() / KM;
            int rows = KM;
            int cols = PM;

            int[,] mat_plain = new int[rows, cols];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    mat_plain[j, i] = plainText[i * rows + j];
                }
            }


            int[,] result = new int[KM, PM];

            // Multiply the matrices
            for (int i = 0; i < KM; i++)
            {
                for (int j = 0; j < PM; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < KM; k++)
                    {
                        sum += mat_key[i, k] * mat_plain[k, j];
                    }
                    result[i, j] = sum % 26;
                }
            }

            List<int> cipher = new List<int>();

            for (int i = 0; i < PM; i++)
            {
                for (int j = 0; j < KM; j++)
                {
                    cipher.Add(result[j, i]);
                }
            }

            return cipher;

        }

        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            List<int> Key = new List<int>();

            for (int i =0; i<3;i++)
            {
                for (int randKey1 = 0; randKey1 < 26; randKey1++)
                {
                    for (int randKey2 = 0; randKey2 < 26; randKey2++)
                    {
                        for (int randKey3 = 0; randKey3 < 26; randKey3++)
                        {
                            if (
                               ((randKey1 * plainText[0]) + (randKey2 * plainText[1]) + (randKey3 * plainText[2])) % 26 == cipherText[i]&&
                               ((randKey1 * plainText[3]) + (randKey2 * plainText[4]) + (randKey3 * plainText[5])) % 26 == cipherText[i+3]&&
                               ((randKey1 * plainText[6]) + (randKey2 * plainText[7]) + (randKey3 * plainText[8])) % 26 == cipherText[i+6]
                               )
                            {
                                Key.Add(randKey1);
                                Key.Add(randKey2);
                                Key.Add(randKey3);
                            }
                        }
                    }
                }
            }
            return Key;
        }

        private int[,] matrix_Trans(int[,] matrix, int m)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = i + 1; j < m; j++)
                {
                    int tmp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = tmp;
                }
            }
            return matrix;
        }
        private int GetDeterminant(int[,] mat_key, int m)
        {
            int determinant = 0;

            if (m == 2)
            {
                determinant = mat_key[0, 0] * mat_key[1, 1] - mat_key[1, 0] * mat_key[0, 1];
                return determinant;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    determinant += (mat_key[0, i] * (mat_key[1, (i + 1) % 3] * mat_key[2, (i + 2) % 3] - mat_key[1, (i + 2) % 3] * mat_key[2, (i + 1) % 3]));
            }
            return determinant;
        }




    }
}
