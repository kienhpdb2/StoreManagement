import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import axios from '../../common/baseAxios';
import CreateAccountDto from './models/CreateAccountDto';
import { GenderEnum } from './models/GenderEnum';
interface IOpenModalProps {
    open: boolean,
    closeForm: Function,
}

const AddModal: React.FC<IOpenModalProps> = (props: IOpenModalProps) => {
    const { open, closeForm } = props;
    const onFinish = (input: CreateAccountDto) => {
        axios.post(`Account`, input).then((res) => {
            let { data } = res;
            if (data && data.success === true) {
                message.success('Tạo tài khoản thành công.')
                closeForm(true);
            }
            else {
                message.error(data.error)
            }
        })
    }
    return <>
        <Modal
            title="THÊM TÀI KHOẢN"
            centered
            open={open}
            onOk={() => closeForm(false)}
            onCancel={() => closeForm(false)}
            footer={null}
            width={1000}
        >
            <Form
                name="basic"
                layout={'vertical'}
                style={{ maxWidth: 1000 }}
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item label="Tên nhân viên" name="fullName" rules={[{ required: true, message: 'Vui lòng nhập tên nhân viên!' }]}>
                    <Input placeholder='Tên nhân viên' />
                </Form.Item>
                <Form.Item name="phoneNumber" label="Số điện thoại" rules={[{ required: true, message: 'Vui lòng nhập số điện thoại !' }]}>
                    <Input placeholder='Số điện thoại' />
                </Form.Item>
                <Form.Item label="Email" name="email" rules={[
                    { required: true, message: 'Vui lòng nhập email của bạn!' },
                    { type: 'email', message: 'Định dạng E-mail không đúng!' },
                ]}>
                    <Input placeholder='Email của bạn' />
                </Form.Item>
                <Form.Item
                    label="Mật khẩu"
                    name="password"
                    rules={[{ required: true, message: 'Vui lòng nhập mật khẩu của bạn!' },
                    {
                        min: 4,
                        message: 'Mật khẩu phải ít nhất 4 kí tự!',
                    }
                    ]}
                >
                    <Input.Password placeholder='Mật khẩu của bạn' />
                </Form.Item>
                <Form.Item
                    name="confirm"
                    label="Xác nhận mật khẩu"
                    dependencies={['password']}
                    hasFeedback
                    rules={[
                        {
                            required: true,
                            message: 'Vui lòng nhập xác nhận mật khẩu của bạn!',
                        },
                        ({ getFieldValue }) => ({
                            validator(_, value) {
                                if (!value || getFieldValue('password') === value) {
                                    return Promise.resolve();
                                }
                                return Promise.reject(new Error('Xác nhận mật khẩu và mật khẩu không đúng!'));
                            },
                        }),
                    ]}
                >
                    <Input.Password placeholder='Xác nhận mật khẩu của bạn' />
                </Form.Item>
                <Form.Item name="gender" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính !' }]}>
                    <Select
                        placeholder="Chọn giới tính"
                        allowClear
                    >
                        <Select.Option value={GenderEnum.Male}>Nam</Select.Option>
                        <Select.Option value={GenderEnum.Female}>Nữ</Select.Option>
                        <Select.Option value={GenderEnum.Unknown}>Khác</Select.Option>
                    </Select>
                </Form.Item>
                <Form.Item
                    label="Ngày sinh"
                    name="dob"
                    rules={[
                        { required: true, message: 'Vui lòng nhập ngày sinh của nhân viên!' },
                    ]}
                >
                    <DatePicker style={{ width: '100%' }} placeholder='Ngày sinh của nhân viên' />
                </Form.Item>
                <Form.Item
                    label="Địa chỉ"
                    name="address"
                    rules={[
                        { required: true, message: 'Vui lòng nhập địa chỉ!' },
                    ]}
                >
                    <Input placeholder='Địa chỉ' />
                </Form.Item>
                <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit" >
                        Lưu
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    </>
}

export default AddModal;