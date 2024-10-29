using NUnit.Framework;
using System;

namespace Bank.Tests
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankClass account;

        [SetUp]
        public void Setup()
        {
            account = new BankClass(100.0); // Initialize account with a balance of 100
        }

        [Test]
        [TestCase(50.0, 150.0)] // Test adding 50 should result in 150 balance
        [TestCase(0.0, 100.0)]  // Test adding 0 should not change the balance
        public void Add_ShouldIncreaseBalance_WhenAmountIsPositive(double amountToAdd, double expectedBalance)
        {
            account.Add(amountToAdd);
            Assert.AreEqual(expectedBalance, account.Balance);
        }

        [Test]
        [TestCase(-10.0)] // Test adding a negative amount
        public void Add_ShouldThrowArgumentOutOfRangeException_WhenAmountIsNegative(double amountToAdd)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Add(amountToAdd));
        }

        [Test]
        [TestCase(30.0, 70.0)] // Test withdrawing 30 should result in 70 balance
        [TestCase(100.0, 0.0)] // Test withdrawing all should result in 0 balance
        public void Withdraw_ShouldDecreaseBalance_WhenAmountIsValid(double amountToWithdraw, double expectedBalance)
        {
            account.Withdraw(amountToWithdraw);
            Assert.AreEqual(expectedBalance, account.Balance);
        }

        [Test]
        [TestCase(200.0)] // Test withdrawing more than the balance
        public void Withdraw_ShouldThrowArgumentOutOfRangeException_WhenAmountIsGreaterThanBalance(double amountToWithdraw)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(amountToWithdraw));
        }

        [Test]
        [TestCase(-10.0)] // Test withdrawing a negative amount
        public void Withdraw_ShouldThrowArgumentOutOfRangeException_WhenAmountIsNegative(double amountToWithdraw)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(amountToWithdraw));
        }

        [Test]
        [TestCase(30.0, 70.0, 80.0)] // Transfer 30, should result in 70 and 80 balance
        [TestCase(200.0, 0.0, 100.0)] // Transfer all, should result in 0 and 100 balance
        public void TransferFundsTo_ShouldTransferFunds_WhenValidAmountAndAccount(double amountToTransfer, double expectedSourceBalance, double expectedTargetBalance)
        {
            var targetAccount = new BankClass(50.0);
            account.TransferFundsTo(targetAccount, amountToTransfer);

            Assert.AreEqual(expectedSourceBalance, account.Balance);
            Assert.AreEqual(expectedTargetBalance, targetAccount.Balance);
        }

        [Test]
        public void TransferFundsTo_ShouldThrowArgumentNullException_WhenTargetAccountIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => account.TransferFundsTo(null, 30.0));
        }

        [Test]
        [TestCase(200.0)] // Test transferring more than the balance
        public void TransferFundsTo_ShouldThrowArgumentOutOfRangeException_WhenTransferAmountIsGreaterThanBalance(double amountToTransfer)
        {
            var targetAccount = new BankClass(50.0);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.TransferFundsTo(targetAccount, amountToTransfer));
        }
    }
}
